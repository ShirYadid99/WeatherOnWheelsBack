//כל ההגדרות לחיבור למסד Mongo
namespace WeatherOnWheels.Models
{
    public class WeatherDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string PlacesCollectionName { get; set; }
    }
}
