using MongoDB.Driver;
using System.Net.Http;
using System.Text.Json;
using System.Web;
using WeatherOnWheels.Dto;
using WeatherOnWheels.Models;

namespace WeatherOnWheels.Services
{

    /// <summary>
    /// Service for managing places, including CRUD operations and geocoding addresses to coordinates.
    /// </summary>
    public class PlaceService
    {
        private readonly IMongoCollection<Place> _placesCollection;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceService"/> class.
        /// </summary>
        /// <param name="database">MongoDB database instance to access the Places collection.</param>
        public PlaceService(IMongoDatabase database)
        {
            _placesCollection = database.GetCollection<Place>("places");
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("WeatherOnWheels/1.0 (yadidshir@gmail.com)");

        }

        /// <summary>
        /// Retrieves all places from the database.
        /// </summary>
        /// <returns>List of all places.</returns>
        public async Task<List<Place>> GetPlacesAsync()
        {
            return await _placesCollection.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific place by its ID.- not used
        /// </summary>
        /// <param name="id">The ID of the place.</param>
        /// <returns>The found place, or null if not found.</returns>
        public async Task<Place> GetPlaceByIdAsync(string id)
        {
            return await _placesCollection.Find(place => place.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Creates a new place. If coordinates are missing but an address is provided, it attempts to fetch the coordinates using Nominatim.
        /// </summary>
        /// <param name="place">The place to create.</param>
        public async Task CreatePlaceAsync(Place place)
        {
            if ((place.Latitude == 0 || place.Longitude == 0) && !string.IsNullOrWhiteSpace(place.Address))
            {
                var coords = await GetCoordinatesFromAddress(place.Address);
                if (coords != null)
                {
                    place.Latitude = coords.Value.lat;
                    place.Longitude = coords.Value.lon;
                }
            }

            await _placesCollection.InsertOneAsync(place);
        }

        /// <summary>
        /// Uses Nominatim (OpenStreetMap) to get latitude and longitude from an address.
        /// </summary>
        /// <param name="address">The address to geocode.</param>
        /// <returns>Tuple of latitude and longitude if found; otherwise, null.</returns>

        private async Task<(double lat, double lon)?> GetCoordinatesFromAddress(string address)
        {
            var encodedAddress = HttpUtility.UrlEncode(address);
            var url = $"https://nominatim.openstreetmap.org/search?q={encodedAddress}&format=json&limit=1";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var results = JsonSerializer.Deserialize<List<NominatimResultDto>>(content);
                    if (results != null && results.Count > 0)
                    {
                        var first = results[0];
                        return (double.Parse(first.lat), double.Parse(first.lon));
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Unexpected error occurred");
            }

            return null;
        }

    }
}
