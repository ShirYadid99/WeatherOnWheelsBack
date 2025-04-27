namespace WeatherOnWheels.Dto
{
    /// <summary>
    /// Data Transfer Object (DTO) for creating a new Place.
    /// </summary>
    public class CreatePlaceDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
    }
}
