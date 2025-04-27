using Microsoft.AspNetCore.Mvc;
using WeatherOnWheels.Models;
using WeatherOnWheels.Services;

namespace WeatherOnWheels.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase
    {
        private readonly PlaceService _placeService;

        public PlaceController(PlaceService placeService)
        {
            _placeService = placeService;
        }

        // POST: api/place
        [HttpPost]
        public async Task<IActionResult> CreatePlace([FromBody] Place place)
        {
            if (place == null)
            {
                return BadRequest("Place data is required.");
            }

            await _placeService.CreatePlaceAsync(place);

            // שימוש ב-Created עם URL ידני (פתרון פשוט ונקי לשגיאת ה-InvalidOperationException)
            return Created($"api/place/{place.Id}", place);
        }

        // GET: api/place
        [HttpGet]
        public async Task<ActionResult<List<Place>>> GetPlaces()
        {
            var places = await _placeService.GetPlacesAsync();
            return Ok(places);
        }

        // GET: api/place/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Place>> GetPlace(string id)
        {
            var place = await _placeService.GetPlaceByIdAsync(id);
            if (place == null)
            {
                return NotFound();
            }
            return Ok(place);
        }
    }
}
