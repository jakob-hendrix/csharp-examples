using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            // init dummy data
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New Orleans",
                    Description = "City of free-wheeling debauchery",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Spooky House",
                            Description = "So spooky"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Hoppin Bar",
                            Description = "So loud"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Baton Rouge",
                    Description = "Administrative capital of the state",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Big Capital Tower",
                            Description = "So tall"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Big Mall",
                            Description = "Locus of money"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Lafayette",
                    Description = "Former cultural capital, now storm-wrecked wasteland",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Cajundome",
                            Description = "Sports happen here"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Blackpot",
                            Description = "Rice-based dishes abide"
                        }
                    }
                }
            };
        }
    }
}
