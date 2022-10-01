using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    //[Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository 
                                  ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities()
        {
            var cities = await _cityInfoRepository.GetCitiesAsync();

            // map to city DTO
            var results = new List<CityWithoutPointsOfInterestDto>();
            foreach (var entity in cities)
            {       
                results.Add(new CityWithoutPointsOfInterestDto
                {
                    Id = entity.Id,
                    Description = entity.Description,
                    Name = entity.Name
                });
            }

            return Ok(results);

            //return Ok(_citiesDataStore.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            // unhandled exceptions return 500
            //var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);
            //if (city == null)
            //{
            //    return NotFound();
            //}
            //return Ok(city);
            return NotFound();
        }
    }
}
