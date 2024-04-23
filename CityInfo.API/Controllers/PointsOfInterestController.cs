using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointsOfInterestController>> GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterests);
        }

        [HttpGet("{pointId}", Name = "GetPointOfInterest")]
        public ActionResult<PointsOfInterestController> GetPointOfInterest(int cityId, int pointId)
        {
            var city = CitiesDataStore.Current.Cities.Find(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterests.FirstOrDefault(p => p.Id == pointId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, CreatePointOfInterestDto createPointOfInterestDto)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null) return NotFound();

            var lastId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterests).Max(p => p.Id);

            var pointOfInterest = new PointOfInterestDto
            { Id = ++lastId, Name = createPointOfInterestDto.Name, Description = createPointOfInterestDto.Description };

            city.PointsOfInterests.Add(pointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new { cityId, pointId = pointOfInterest.Id }, pointOfInterest);
        }

        [HttpPatch("{pointId}")]
        public ActionResult PatchPointOfInterest(int pointId, int cityId, JsonPatchDocument<UpdatePointOfInterestDto> patchDocument)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return NotFound();

            var pointOfInterest = city.PointsOfInterests.FirstOrDefault(p => p.Id == pointId);
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
        public ActionResult DeletePointOfInterest(int pointId, int cityId ) {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault (c => c.Id == cityId);
            if (city == null) return NotFound();

            var pointToDelete = city.PointsOfInterests.FirstOrDefault(p =>p.Id == pointId);
            if (pointToDelete == null) return NotFound();

            city.PointsOfInterests.Remove(pointToDelete);

            return NoContent();
        }
    }
}
