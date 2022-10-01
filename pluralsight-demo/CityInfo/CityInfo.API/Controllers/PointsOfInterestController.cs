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

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
            int cityId,
            PointOfInterestCreationDto pointOfInterest)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found when accessing points of interest.");
                return NotFound();
            }
            var finalPoint = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPoint); // the save will also fill out ids

            var createdPointToReturn = _mapper.Map<Models.PointOfInterestDto>(finalPoint);
            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = createdPointToReturn.Id
                },
                createdPointToReturn);
        }

        [HttpPut("{pointofinterestId}")]
        public async Task<ActionResult> UpdatePointOfInterest(
            int cityId,
            int pointOfInterestId,
            PointOfInterestUpdateDto pointOfInterest)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found when accessing points of interest.");
                return NotFound();
            }

            var pointEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, pointEntity);
            await _cityInfoRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{pointofinterestId}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(
            int cityId,
            int pointOfInterestId,
            JsonPatchDocument<PointOfInterestUpdateDto> patchDocument)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found when accessing points of interest.");
                return NotFound();
            }

            var savedPointOfInterest = 
                await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (savedPointOfInterest == null)
            {
                return NotFound();
            }

            // need to map to update dto

            // Create an intermediate update DTO that defaults to the current value
            var pointOfInterestToPatch =
                _mapper.Map<PointOfInterestUpdateDto>(savedPointOfInterest);


            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(pointOfInterestToPatch, savedPointOfInterest);
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{pointofinterestId}")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found when accessing points of interest.");
                return NotFound();
            }

            var savedPointOfInterest =
                await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (savedPointOfInterest == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(savedPointOfInterest);
            await _cityInfoRepository.SaveChangesAsync();

            _mailService.Send(
                "Point of interest deleted",
                $"Point of interest {savedPointOfInterest.Name} (id {savedPointOfInterest.Id}) was deleted.");

            return NoContent();
        }
    }
}
