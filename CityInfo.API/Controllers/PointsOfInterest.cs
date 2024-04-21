using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterest : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointsOfInterest>> GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterests);
        }

        [HttpGet]
        [Route("{pointId}")]
        public ActionResult<PointsOfInterest> GetPointOfInterest(int cityId, int pointId)
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
    }
}
