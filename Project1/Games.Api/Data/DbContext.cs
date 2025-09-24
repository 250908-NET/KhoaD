using Microsoft.EntityFrameworkCore;
using Games.Models;

namespace Games.Data;


public class GamesDbContext  : DbContext
{
    // Constructor
    public GamesDbContext (DbContextOptions<GamesDbContext > options) : base(options) {}

    // DbSets (tables)
    public DbSet<Game> Games { get; set; } = null!;

    //  Model configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<Game>()
            .Property(g => g.Title)
            .IsRequired()
            .HasMaxLength(100);

        
        modelBuilder.Entity<Game>()
            .Property(g => g.Developer)
            .HasMaxLength(100);
    }
}