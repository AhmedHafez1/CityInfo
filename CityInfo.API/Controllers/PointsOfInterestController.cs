using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;


        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            CitiesDataStore citiesData,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger;
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            var cityExists = await _cityInfoRepository.CityExistsAsync(cityId);

            if (!cityExists)
            {
                _logger.LogInformation($"City with id {cityId} doesn't exist!");
                return NotFound();
            }

            var pointsOfInterest = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterest));
        }

        [HttpGet("{pointId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointId)
        {
            var cityExists = await _cityInfoRepository.CityExistsAsync(cityId);
            if (!cityExists)
            {
                return NotFound();
            }

            var pointOfInterest = await _cityInfoRepository.GetOnePointOfInterestAsync(cityId, pointId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, CreatePointOfInterestDto createPointOfInterestDto)
        {
            var cityExists = await _cityInfoRepository.CityExistsAsync(cityId);

            if (!cityExists) return NotFound();

            var pointOfInterest = await _cityInfoRepository.AddPointOfInterestAsync(cityId, _mapper.Map<PointOfInterest>(createPointOfInterestDto));

            return CreatedAtRoute("GetPointOfInterest", new { cityId, pointId = pointOfInterest.Id }, _mapper.Map<PointOfInterestDto>(pointOfInterest));
        }

        [HttpPatch("{pointId}")]
        public ActionResult PatchPointOfInterest(int pointId, int cityId, JsonPatchDocument<UpdatePointOfInterestDto> patchDocument)
        {
            //var city = _citiesData.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null) return NotFound();

            //var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointId);
            //if (pointOfInterest == null) return NotFound();

            //var pointToUpdate = new UpdatePointOfInterestDto
            //{
            //    Name = pointOfInterest.Name,
            //    Description = pointOfInterest.Description,
            //};

            //patchDocument.ApplyTo(pointToUpdate, ModelState);

            //if (!ModelState.IsValid) return BadRequest(ModelState);

            //if (!TryValidateModel(pointToUpdate)) return BadRequest(ModelState);

            //pointOfInterest.Name = pointToUpdate.Name;
            //pointOfInterest.Description = pointToUpdate.Description;

            return NoContent();
        }

        [HttpDelete("{pointId}")]
        public ActionResult DeletePointOfInterest(int pointId, int cityId)
        {
            //var city = _citiesData.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null) return NotFound();

            //var pointToDelete = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointId);
            //if (pointToDelete == null) return NotFound();

            //city.PointsOfInterest.Remove(pointToDelete);

            return NoContent();
        }
    }
}
