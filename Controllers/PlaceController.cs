using Microsoft.AspNetCore.Mvc;
using WeatherOnWheels.Models;
using WeatherOnWheels.Services;

namespace WeatherOnWheels.Controllers
{
    /// <summary>
    /// Controller responsible for managing Places (Create, Get All, Get By Id).
    /// API Route: api/place
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase
    {
        private readonly PlaceService _placeService;

        /// <summary>
        /// Constructor - Injects PlaceService to handle business logic.
        /// </summary>
        /// <param name="placeService">Injected PlaceService</param>

        public PlaceController(PlaceService placeService)
        {
            _placeService = placeService;
        }

        /// <summary>
        /// Creates a new Place.
        /// </summary>
        /// <param name="place">The place object sent from the client.</param>
        /// <returns>201 Created with the created Place or 400 Bad Request if input is invalid.</returns>

        // POST: api/place
        [HttpPost]
        public async Task<IActionResult> CreatePlace([FromBody] Place place)
        {
            if (place == null)
            {
                return BadRequest("Place data is required.");
            }

            await _placeService.CreatePlaceAsync(place);

            return Created($"api/place/{place.Id}", place);
        }

        /// <summary>
        /// Retrieves all Places.
        /// </summary>
        /// <returns>List of all saved Places (200 OK).</returns>

        // GET: api/place
        [HttpGet]
        public async Task<ActionResult<List<Place>>> GetPlaces()
        {
            var places = await _placeService.GetPlacesAsync();
            return Ok(places);
        }


        /// <summary>
        /// Retrieves a specific Place by its ID.- not used
        /// </summary>
        /// <param name="id">The ID of the Place.</param>
        /// <returns>200 OK with the Place if found, or 404 Not Found if not found.</returns>

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
