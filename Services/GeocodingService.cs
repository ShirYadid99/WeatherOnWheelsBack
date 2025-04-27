/*using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherOnWheels.Dto;


namespace WeatherOnWheels.Services
{

    public class GeocodingService
    {
        private readonly HttpClient _httpClient;

        public GeocodingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://nominatim.openstreetmap.org/");
        }

        public async Task<(double lat, double lon)?> GetCoordinatesAsync(string address)
        {
            var url = $"search?q={Uri.EscapeDataString(address)}&format=json&limit=1";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var results = JsonSerializer.Deserialize<List<NominatimResultDto>>(json);

            if (results != null && results.Count > 0)
            {
                var result = results[0];
                return (double.Parse(result.lat), double.Parse(result.lon));
            }

            return null;
        }

    }

}
*/