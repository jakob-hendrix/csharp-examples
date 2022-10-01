using AutoMapper;
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
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository 
                                  ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities()
        {
            var cities = await _cityInfoRepository.GetCitiesAsync();
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cities));

            // manually map to city DTO
            //var results = new List<CityWithoutPointsOfInterestDto>();
            //foreach (var entity in cities)
            //{       
            //    results.Add(new CityWithoutPointsOfInterestDto
            //    {
            //        Id = entity.Id,
            //        Description = entity.Description,
            //        Name = entity.Name
            //    });
            //}

            //return Ok(results);

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
