using MongoDB.Driver;
using System.Net.Http;
using System.Text.Json;
using System.Web;
using WeatherOnWheels.Dto;
using WeatherOnWheels.Models;

namespace WeatherOnWheels.Services
{
    public class PlaceService
    {
        private readonly IMongoCollection<Place> _placesCollection;
        private readonly HttpClient _httpClient;

        public PlaceService(IMongoDatabase database)
        {
            _placesCollection = database.GetCollection<Place>("places");
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("WeatherOnWheels/1.0 (yadidshir@gmail.com)");

        }

        public async Task<List<Place>> GetPlacesAsync()
        {
            return await _placesCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Place> GetPlaceByIdAsync(string id)
        {
            return await _placesCollection.Find(place => place.Id == id).FirstOrDefaultAsync();
        }

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
                Console.WriteLine($"Unexpected error occurred: {ex.Message}");
            }

            return null;
        }

    }
}
