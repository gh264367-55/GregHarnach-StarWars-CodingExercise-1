using GregHarnach_starWars_CodingExercise.Models;
using Microsoft.EntityFrameworkCore;


namespace GregHarnach_starWars_CodingExercise.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Starship> Starships => Set<Starship>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Starship>(e =>
            {
                e.Property(p => p.CostInCredits);
                e.Property(p => p.Length);
                e.Property(p => p.HyperdriveRating);
                e.HasIndex(p => p.Name);
            });
        }
    }
}
