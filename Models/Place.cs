using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WeatherOnWheels.Models
{
    /// <summary>
    /// Represents a place entity stored in the MongoDB database,
    /// including geolocation and creation time information.
    /// </summary>
    public class Place
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }


        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }  // Restaurant, Hotel, Park

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("latitude")]
        public double Latitude { get; set; }

        [BsonElement("longitude")]
        public double Longitude { get; set; }
    }
}
