using CityInfo.API.Models;
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
        private readonly CitiesDataStore _citiesData;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            CitiesDataStore citiesData)
        {
            _logger = logger;
            _citiesData = citiesData;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointsOfInterestController>> GetPointsOfInterest(int cityId)
        {
            var city = _citiesData.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                _logger.LogInformation($"City with id {cityId} doesn't exist!");
                return NotFound();
            }

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{pointId}", Name = "GetPointOfInterest")]
        public ActionResult<PointsOfInterestController> GetPointOfInterest(int cityId, int pointId)
        {
            var city = _citiesData.Cities.Find(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, CreatePointOfInterestDto createPointOfInterestDto)
        {
            var city = _citiesData.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null) return NotFound();

            var lastId = _citiesData.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            var pointOfInterest = new PointOfInterestDto
            { Id = ++lastId, Name = createPointOfInterestDto.Name, Description = createPointOfInterestDto.Description };

            city.PointsOfInterest.Add(pointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new { cityId, pointId = pointOfInterest.Id }, pointOfInterest);
        }

        [HttpPatch("{pointId}")]
        public ActionResult PatchPointOfInterest(int pointId, int cityId, JsonPatchDocument<UpdatePointOfInterestDto> patchDocument)
        {
            var city = _citiesData.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return NotFound();

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointId);
            if (pointOfInterest == null) return NotFound();

            var pointToUpdate = new UpdatePointOfInterestDto
            {
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description,
            };

            patchDocument.ApplyTo(pointToUpdate, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!TryValidateModel(pointToUpdate)) return BadRequest(ModelState);

            pointOfInterest.Name = pointToUpdate.Name;
            pointOfInterest.Description = pointToUpdate.Description;

            return NoContent();
        }

        [HttpDelete("{pointId}")]
        public ActionResult DeletePointOfInterest(int pointId, int cityId)
        {
            var city = _citiesData.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return NotFound();

            var pointToDelete = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointId);
            if (pointToDelete == null) return NotFound();

            city.PointsOfInterest.Remove(pointToDelete);

            return NoContent();
        }
    }
}
