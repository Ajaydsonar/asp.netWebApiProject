using GameStroreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStroreApi.Data;
public class GameStoreContext(DbContextOptions<GameStoreContext> options)
     : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(

            new { Id = 1, Name = "Fighting" },
            new { Id = 2, Name = "Action" },
            new { Id = 3, Name = "Adventure" },
            new { Id = 4, Name = "Role-Playing" },
            new { Id = 5, Name = "Strategy" },
            new { Id = 6, Name = "Simulation" },
            new { Id = 7, Name = "Sports" },
            new { Id = 8, Name = "Racing" },
            new { Id = 9, Name = "Puzzle" },
            new { Id = 10, Name = "Platformer" }
        );
    }
}