using AutoMapper;
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
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities([FromQuery] string? name, [FromQuery] string? search)
        {
            var cities = await _cityInfoRepository.GetCitiesAsync(name, search);

            return Ok(_mapper.Map<IEnumerable<CityDto>>(cities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetCity(int id)
        {
            var city = await _cityInfoRepository.GetCityAsync(id);

            if (city == null)
            {
                return NotFound("City not found with id " + id);
            }

            return Ok(_mapper.Map<CityDto>(city));
        }
    }
}
