//כל ההגדרות לחיבור למסד Mongo
namespace WeatherOnWheels.Models
{
    /// <summary>
    /// Represents the configuration settings required to connect to the MongoDB database.
    /// These settings are typically loaded from the appsettings.json file.
    /// </summary>
    public class WeatherDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string PlacesCollectionName { get; set; }
    }
}
