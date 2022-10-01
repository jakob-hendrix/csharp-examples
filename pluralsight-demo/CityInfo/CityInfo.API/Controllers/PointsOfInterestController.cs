using System.Collections;
using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(
            ILogger<PointsOfInterestController> logger, 
            IMailService mailService,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger 
                ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService 
                ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository
                ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper
                ?? throw new ArgumentException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            // does city exist? If not, 404, but if no points, that's fine
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found when accessing points of interest.");
                return NotFound();
            }
            var points = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);
            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(points));

            //try
            //{
            //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //    if (city == null)
            //    {
            //        _logger.LogInformation($"City with id {cityId} was not found when accessing points of interest.");
            //        return NotFound();
            //    }

            //    return Ok(city.PointsOfInterest);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
            //    return StatusCode(500, "A problem occurred while handling your request.");
            //}
        }

        [HttpGet("{pointofinterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            // does city exist? If not, 404, but if no points, that's fine
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found when accessing points of interest.");
                return NotFound();
            }

            var point = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (point == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(point));

            //var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            //if (pointOfInterest == null)
            //{
            //    return NotFound();
            //}

            //return Ok(pointOfInterest);
        }

        //[HttpPost]
        //public ActionResult<PointOfInterestDto> CreatePointOfInterest(
        //    int cityId, 
        //    PointOfInterestCreationDto pointOfInterest)
        //{
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
            
        //    // TODO: improved later. Pulling all points is slow and will not handle concurrent hits
        //    var maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

        //    var finalPointOfInterest = new PointOfInterestDto()
        //    {
        //        Id = ++maxPointOfInterestId,
        //        Name = pointOfInterest.Name,
        //        Description = pointOfInterest.Description
        //    };

        //    city.PointsOfInterest.Add(finalPointOfInterest);

        //    return CreatedAtRoute("GetPointOfInterest", 
        //        new
        //        {
        //            cityId = cityId,
        //            pointOfInterestId = finalPointOfInterest.Id
        //        },
        //        finalPointOfInterest
        //        );
        //}

        //[HttpPut("{pointofinterestId}")]
        //public ActionResult<PointOfInterestDto> UpdatePointOfInterest(
        //    int cityId, 
        //    int pointOfInterestId, 
        //    PointOfInterestUpdateDto pointOfInterest)
        //{
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //    var savedPointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        //    if (savedPointOfInterest == null)
        //    {
        //        return NotFound();
        //    }

        //    // consumer of API must provide values for all fields. If not provided, set to default
        //    savedPointOfInterest.Name = pointOfInterest.Name;
        //    savedPointOfInterest.Description = pointOfInterest.Description;

        //    return NoContent();
        //}

        //[HttpPatch("{pointofinterestId}")]
        //public ActionResult PartiallyUpdatePointOfInterest(
        //    int cityId,
        //    int pointOfInterestId,
        //    JsonPatchDocument<PointOfInterestUpdateDto> patchDocument)
        //{
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //    var savedPointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        //    if (savedPointOfInterest == null)
        //    {
        //        return NotFound();
        //    }

        //    // Create an intermediate update DTO that defaults to the current value
        //    var pointOfInterestToPatch =
        //        new PointOfInterestUpdateDto()
        //        {
        //            Name = savedPointOfInterest.Name,
        //            Description = savedPointOfInterest.Description
        //        };
            
        //    patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    // Complete the update
        //    savedPointOfInterest.Name = pointOfInterestToPatch.Name;
        //    savedPointOfInterest.Description = pointOfInterestToPatch.Description;

        //    return NoContent();
        //}

        //[HttpDelete("{pointofinterestId}")]
        //public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId) 
        //{
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        //    if (pointOfInterest == null)
        //    {
        //        return NotFound();
        //    }

        //    city.PointsOfInterest.Remove(pointOfInterest);
        //    _mailService.Send(
        //        "Point of interest deleted",
        //        $"Point of interest {pointOfInterest.Name} (id {pointOfInterest.Id}) was deleted from {city.Name}  (id {city.Id}).");
        //    return NoContent();
        //}
    }
}
