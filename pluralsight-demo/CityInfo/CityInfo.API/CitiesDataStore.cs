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
                    Description = "City of free-wheeling debauchery"
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Baton Rouge",
                    Description = "Administrative capital of the state"
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Lafayette",
                    Description = "Former cultural capital, now storm-wrecked wasteland"
                }
            };
        }
    }
}
