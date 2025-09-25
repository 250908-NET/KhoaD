using Microsoft.EntityFrameworkCore;
using Games.Models;

namespace Games.Data;


public class GamesDbContext  : DbContext
{
    // Constructor
    public GamesDbContext (DbContextOptions<GamesDbContext > options) : base(options) {}

    // DbSets (tables)
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Platform> Platforms { get; set; } = null!;
    public DbSet<GamePlatform> GamePlatforms { get; set; } = null!;

    //  Model configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Composite key for join table
        modelBuilder.Entity<GamePlatform>()
            .HasKey(gc => new { gc.GameId, gc.PlatformId });
            
        // Relationships
        modelBuilder.Entity<GamePlatform>()
            .HasOne(gp => gp.Game)
            .WithMany(g => g.GamePlatforms)
            .HasForeignKey(gp => gp.GameId);

        modelBuilder.Entity<GamePlatform>()
            .HasOne(gp => gp.Platform)
            .WithMany(p => p.GamePlatforms)
            .HasForeignKey(gp => gp.PlatformId);
    }
}