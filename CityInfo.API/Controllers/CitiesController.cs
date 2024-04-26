using CityInfo.API.Models;
using CityInfo.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities()
        {
            var cities = await _cityInfoRepository.GetCitiesAsync();

            var citiesDtos = cities.Select(c => new CityDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                PointsOfInterest = c.PointsOfInterest.Select(p => new PointOfInterestDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,

                }).ToList()
            });

            return Ok(citiesDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetCity(int id)
        {
            var city = await _cityInfoRepository.GetCityAsync(id);

            if (city == null)
            {
                return NotFound("City not found with id " + id);
            }

            var cityDto = new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description,
                PointsOfInterest = city.PointsOfInterest.Select(p =>
                new PointOfInterestDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                }).ToList()
            };

            return Ok(cityDto);
        }
    }
}
