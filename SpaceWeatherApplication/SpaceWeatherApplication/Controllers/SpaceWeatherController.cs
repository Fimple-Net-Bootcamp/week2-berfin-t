using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;

namespace SpaceWeatherApplication.Controllers
{
    [ApiController]
    [Route("api/v1/space-weather")]
    public class SpaceWeatherController : ControllerBase
    {
        private static List<SpaceWeather> spaceWeatherList = new List<SpaceWeather>();
        private static int idCounter = 1;

        [HttpPost]
        public IActionResult Create(SpaceWeather spaceWeather)
        {
            spaceWeather.Id = idCounter++;
            spaceWeatherList.Add(spaceWeather);
            // 201 Created for a successful POST operation
            return CreatedAtAction(nameof(GetById), new { id = spaceWeather.Id }, spaceWeather);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // 200 OK for a successful GET operation
            return Ok(spaceWeatherList);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var spaceWeather = spaceWeatherList.Find(sw => sw.Id == id);
            if (spaceWeather == null)
            {
                // 404 Not Found if the resource is not found
                return NotFound();
            }
            // 200 OK for a successful GET operation
            return Ok(spaceWeather);
        }


        [HttpGet("{id}/planetDataList")]
        public IActionResult GetByPlanetDataList(int id, [FromQuery] string? sortOrder = null)
        {
            var spaceWeather = spaceWeatherList.Find(sw => sw.Id == id);
            if (spaceWeather == null)
            {
                // 404 Not Found if the resource is not found
                return NotFound();
            }
            switch (sortOrder)
            {
                case "desc":
                    return Ok(spaceWeather.PlanetDataList.OrderByDescending(i => i.PlanetName));
                case "asc":
                    return Ok(spaceWeather.PlanetDataList.OrderBy(i => i.PlanetName));
                default:
                    return Ok(spaceWeather.PlanetDataList);
            }
        }

        // PUT: Used to update an existing resource. Usually used to update the full resource.
        [HttpPut("{id}")]
        public IActionResult Update(int id, SpaceWeather updatedSpaceWeather)
        {
            var spaceWeather = spaceWeatherList.Find(sw => sw.Id == id);
            if (spaceWeather == null)
            {
                // 404 Not Found if the resource is not found
                return NotFound();
            }

            spaceWeather.Date = updatedSpaceWeather.Date;
            spaceWeather.PlanetDataList = updatedSpaceWeather.PlanetDataList;

            // 204 No Content for a successful PUT operation with no response body
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartialUpdateSpaceWeather(int id, JsonPatchDocument<SpaceWeather> patch)
        {
            var spaceWeather = spaceWeatherList.Find(sw => sw.Id == id);
            if (spaceWeather == null)
            {
                // 404 Not Found if the resource is not found
                return NotFound();
            }

            // Partial update operations by applying JsonPatchDocument
            try
            {
                patch.ApplyTo(spaceWeather);
            }
            catch (JsonPatchException ex)
            {
                // Handle any potential errors here
                ModelState.AddModelError("JsonPatch", ex.Message);
                // 400 Bad Request for a client error (e.g., invalid JSON Patch format)
                return BadRequest(ModelState);
            }
            // 204 No Content for a successful PATCH operation with no response body
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var spaceWeather = spaceWeatherList.Find(sw => sw.Id == id);
            if (spaceWeather == null)
            {
                // 404 Not Found if the resource is not found
                return NotFound();
            }

            spaceWeatherList.Remove(spaceWeather);
            // 204 No Content for a successful DELETE operation with no response body
            return NoContent();
        }
    }
}