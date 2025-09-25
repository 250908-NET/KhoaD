
using Microsoft.EntityFrameworkCore;
using Games.Data;
using Games.Models;
using Games.Services;
using Games.Repositories;
using Games.DTOs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Connection string
string CS = File.ReadAllText("../ConnectionString.txt");


// Add services to the container
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core DbContext
builder.Services.AddDbContext<GamesDbContext>(options => options.UseSqlServer(CS));

// Repositories
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

// Services
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();

// Setup Serilog
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

// -----------------------------------------------------
// Minimal API Endpoints
// -----------------------------------------------------

app.MapGet("/", () =>
{
    return "Hello World";
});

// -----------------------------------------------------
// GAMES ENDPOINTS
// -----------------------------------------------------

// GET: receive all games
app.MapGet("/games", async (GamesDbContext db) =>
{
    var games = await db.Games
        .Include(g => g.GamePlatforms)
        .ThenInclude(gp => gp.Platform)
        .ToListAsync();

    return Results.Ok(games.Select(g => g.ToDto()));
});


// GET: receive game by id
app.MapGet("/games/{id}", async (GamesDbContext db, int id) =>
{
    var game = await db.Games
        .Include(g => g.GamePlatforms)
        .ThenInclude(gp => gp.Platform)
        .FirstOrDefaultAsync(g => g.GameId == id);

    return game is null ? Results.NotFound() : Results.Ok(game.ToDto());
});

// POST: add game
app.MapPost("/games", async (GamesDbContext db, CreateGameDto dto) =>
{
    var game = new Game
    {
        Title = dto.Title,
        Developer = dto.Developer,
        ReleaseYear = dto.ReleaseYear
    };

    db.Games.Add(game);
    await db.SaveChangesAsync();

    return Results.Created($"/games/{game.GameId}", game.ToDto());
});

// PUT: update game
app.MapPut("/games/{id}", async (GamesDbContext db, int id, UpdateGameDto dto) =>
{
    var game = await db.Games.FindAsync(id);
    if (game is null) return Results.NotFound();

    game.Title = dto.Title;
    game.Developer = dto.Developer;
    game.ReleaseYear = dto.ReleaseYear;

    await db.SaveChangesAsync();
    return Results.Ok(game.ToDto());
});

// DELETE: delete game
app.MapDelete("/games/{id}", async (IGameService service, int id) =>
{
    var game = await service.GetByIdAsync(id);
    if (game is null)
    {
        return Results.NotFound("Game not found");
    }

    await service.DeleteAsync(id);
    return Results.NoContent();
});

// -----------------------------------------------------
// Platform Endpoints
// GET: receive all platforms
app.MapGet("/platforms", async (GamesDbContext db) =>
{
    var platforms = await db.Platforms
        .Include(p => p.GamePlatforms)
        .ThenInclude(gp => gp.Game)
        .ToListAsync();

    return Results.Ok(platforms.Select(p => p.ToDto()));
});

// GET: receive platform by id
app.MapGet("/platforms/{id}", async (GamesDbContext db, int id) =>
{
    var platform = await db.Platforms
        .Include(p => p.GamePlatforms)
        .ThenInclude(gp => gp.Game)
        .FirstOrDefaultAsync(p => p.PlatformId == id);

    return platform is null ? Results.NotFound() : Results.Ok(platform.ToDto());
});

// POST: create new platform
app.MapPost("/platforms", async (GamesDbContext db, CreatePlatformDto dto) =>
{
    var platform = new Platform
    {
        Name = dto.Name,
        Manufacturer = dto.Manufacturer,
        ReleaseYear = dto.ReleaseYear
    };

    db.Platforms.Add(platform);
    await db.SaveChangesAsync();

    return Results.Created($"/platforms/{platform.PlatformId}", platform.ToDto());
});

// PUT: update platform
app.MapPut("/platforms/{id}", async (GamesDbContext db, int id, UpdatePlatformDto dto) =>
{
    var platform = await db.Platforms.FindAsync(id);
    if (platform is null) return Results.NotFound();

    platform.Name = dto.Name;
    platform.Manufacturer = dto.Manufacturer;
    platform.ReleaseYear = dto.ReleaseYear;

    await db.SaveChangesAsync();
    return Results.Ok(platform.ToDto());
});

// DELETE: delete platform
app.MapDelete("/platforms/{id}", async (IPlatformService service, int id) =>
{
    var platform = await service.GetByIdAsync(id);
    if (platform is null) return Results.NotFound();

    await service.DeleteAsync(id);
    return Results.NoContent();
});

// -----------------------------------------------------
// Link game to a platform
app.MapPost("/games/{gameId}/platforms/{platformId}", async (GamesDbContext db, int gameId, int platformId) =>
{
    var game = await db.Games.FindAsync(gameId);
    var platform = await db.Platforms.FindAsync(platformId);

    if (await db.Games.FindAsync(gameId) is null)
    {
        return Results.NotFound("Game not found");
    }

    if (await db.Platforms.FindAsync(platformId) is null)
    {
        return Results.NotFound("Platform not found");
    }

    if (await db.GamePlatforms.FindAsync(gameId, platformId) is null)
    {
        db.GamePlatforms.Add(new GamePlatform { GameId = gameId, PlatformId = platformId });
        await db.SaveChangesAsync();
    }

    return Results.Ok(new
    {
        Message = $"Linked successfully",
        Game = game.Title,
        Platform = platform.Name
    });
});

// Get all platforms for a game
app.MapGet("/games/{gameId}/platforms", async (GamesDbContext db, int gameId) =>
{
    var game = await db.Games
        .Include(g => g.GamePlatforms)
        .ThenInclude(gp => gp.Platform)
        .FirstOrDefaultAsync(g => g.GameId == gameId);

    if (game is null)
    {
        return Results.NotFound("Game not found");
    }

    var platforms = game.GamePlatforms
        .Select(gp => new PlatformDto
        {
            PlatformId = gp.Platform.PlatformId,
            Name = gp.Platform.Name,
            Manufacturer = gp.Platform.Manufacturer,
            ReleaseYear = gp.Platform.ReleaseYear
        });

    return Results.Ok(platforms);
});

// Get all games for a platform
app.MapGet("/platforms/{platformId}/games", async (GamesDbContext db, int platformId) =>
{
    var platform = await db.Platforms
        .Include(p => p.GamePlatforms)
        .ThenInclude(gp => gp.Game)
        .FirstOrDefaultAsync(p => p.PlatformId == platformId);

    if (platform is null)
    {
        return Results.NotFound("Platform not found");
    }

    var games = platform.GamePlatforms
        .Select(gp => new GameDto
        {
            GameId = gp.Game.GameId,
            Title = gp.Game.Title,
            Developer = gp.Game.Developer,
            ReleaseYear = gp.Game.ReleaseYear
        });

    return Results.Ok(games);
});


app.Run();
