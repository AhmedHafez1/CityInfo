using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Data
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("Ghaza") { Id = 1, Description = "Best City" },
                new City("Quds") { Id = 2, Description = "Best City" },
                new City("Giza") { Id = 3, Description = "Good City" }
                );

            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Beach") { Id = 1, Description = "Best City", CityId = 1 },
                new PointOfInterest("Garden") { Id = 2, Description = "Best City", CityId = 2 },
                new PointOfInterest("Pyranids") { Id = 3, Description = "Good City", CityId = 3 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
