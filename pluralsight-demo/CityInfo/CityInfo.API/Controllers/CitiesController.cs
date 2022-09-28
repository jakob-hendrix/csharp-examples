using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    //[Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int Id)
        {
            // unhandled exceptions return 500
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == Id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}
