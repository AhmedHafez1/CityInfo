﻿using Asp.Versioning;
using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/v{vesion:apiVersion}/cities")]
    [ApiVersion("1.0")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        const int maxPageSize = 33;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities(
            [FromQuery] string? name,
            [FromQuery] string? search,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
            )
        {
            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }

            var (cities, paginationData) = await _cityInfoRepository.GetCitiesAsync(name, search, pageNumber, pageSize);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationData));

            return Ok(_mapper.Map<IEnumerable<CityDto>>(cities));
        }

        /// <summary>
        /// Get a city by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
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
