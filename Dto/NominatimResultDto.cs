namespace WeatherOnWheels.Dto
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a single result from the Nominatim geocoding API.
    /// </summary>
    public class NominatimResultDto
    {
        public string lat { get; set; }
        public string lon { get; set; }
    }
}
