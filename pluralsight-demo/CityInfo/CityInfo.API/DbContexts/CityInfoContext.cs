using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        // Using the constructor method, we can set this up the DI container
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // can manually construct models here

            modelBuilder.Entity<City>().HasData(
                new City("New Orleans")
                {
                    Id = 1,
                    Description = "City of free-wheeling debauchery"
                },
                new City("Baton Rouge")
                {
                    Id = 2,
                    Description = "Administrative capital of the state"
                },
                new City("Lafayette")
                {
                    Id = 3,
                    Description = "Former cultural capital, now storm-wrecked wasteland"
                });

            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Spooky House")
                {
                    Id = 1,
                    Description = "So scary",
                    CityId = 1
                },
                new PointOfInterest("Hopping Bar")
                {
                    Id = 2,
                    Description = "So loud",
                    CityId = 1
                },
                new PointOfInterest("Big Capital Tower")
                {
                    Id = 3,
                    Description = "So tall",
                    CityId = 2
                },
                new PointOfInterest("Big Mall")
                {
                    Id = 4,
                    Description = "So busy",
                    CityId = 2
                },
                new PointOfInterest("Big Dome")
                {
                    Id = 5,
                    Description = "So raucous",
                    CityId = 3
                },
                new PointOfInterest("Black Pot")
                {
                    Id = 6,
                    Description = "So delicious",
                    CityId = 3
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
