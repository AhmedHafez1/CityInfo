using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly CitiesDataStore _citiesData;

        public CitiesController(CitiesDataStore citiesData)
        {
            _citiesData = citiesData;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return _citiesData.Cities;
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var city = _citiesData.Cities.FirstOrDefault(c => c.Id == id);

            if (city == null)
            {
                return NotFound("City not found with id " + id);
            }

            return city;
        }
    }
}
