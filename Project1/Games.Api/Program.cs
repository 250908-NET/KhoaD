
using Microsoft.EntityFrameworkCore;
using Games.Data;
using Games.Models;
using Games.Services;
using Games.Repositories;
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

// Services
builder.Services.AddScoped<IGameService, GameService>();

// Setup Serilog
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger(); // read from appsettings.json
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

// GET: all games
app.MapGet("/games", async (IGameService service) =>
{
    var games = await service.GetAllAsync();
    return Results.Ok(games);
});

// GET: game by id
app.MapGet("/games/{id}", async (IGameService service, int id) =>
{
    var game = await service.GetByIdAsync(id);
    return game is not null ? Results.Ok(game) : Results.NotFound();
});

// POST: create new game
app.MapPost("/games", async (IGameService service, Game game) =>
{
    await service.CreateAsync(game);
    return Results.Created($"/games/{game.GameId}", game);
});


app.Run();
