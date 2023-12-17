using Microsoft.AspNetCore.Mvc;
using SpaceWeatherApplication.Services;
using SpaceWeatherApplication.Entities;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWeatherApplication.Controllers
{
    [ApiController]
    [Route("api/v1/space-weather")]
    public class SpaceWeatherController : ControllerBase
    {
        private readonly SpaceWeatherService _spaceWeatherService;

        public SpaceWeatherController(SpaceWeatherService spaceWeatherService)
        {
            _spaceWeatherService = spaceWeatherService;
        }

        [HttpPost]
        public IActionResult Create(SpaceWeather spaceWeather)
        {
            var createdSpaceWeather = _spaceWeatherService.Create(spaceWeather);
            return CreatedAtAction(nameof(GetById), new { id = createdSpaceWeather.Id }, createdSpaceWeather);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var spaceWeatherList = _spaceWeatherService.GetAll();
            return Ok(spaceWeatherList);
        }

        [HttpGet("{id}")] 
        public IActionResult GetById(int id)
        {
            var spaceWeather = _spaceWeatherService.GetById(id);
            if (spaceWeather == null)
            {
                return NotFound();
            }
            return Ok(spaceWeather);
        }

        [HttpGet("{id}/planetDataList")] 
        public IActionResult GetPlanetDataList(int id, [FromQuery] string? sortOrder = null)
        {
            var planetDataList = _spaceWeatherService.GetPlanetDataList(id, sortOrder);
            if (planetDataList == null)
            {
                return NotFound();
            }
            return Ok(planetDataList);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, SpaceWeather updatedSpaceWeather)
        {
            var success = _spaceWeatherService.Update(id, updatedSpaceWeather);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartialUpdateSpaceWeather(int id, JsonPatchDocument<SpaceWeather> patch)
        {
            var success = _spaceWeatherService.PartialUpdateSpaceWeather(id, patch);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _spaceWeatherService.Delete(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
