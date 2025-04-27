# Weather On Wheels - Backend

This is the C# (.NET) backend for the "Weather on Wheels" project.

## Features

- Add new places to MongoDB.
- Retrieve a list of places.
- Geocode addresses to coordinates using OpenStreetMap Nominatim.
- Data structure includes name, type, address, latitude, longitude, created_at.

## Tech Stack

- C# (.NET 6/7)
- MongoDB
- REST API

## Setup Instructions

1. Clone the repository:
2.Set your MongoDB connection string in appsettings.json:
{
  "MongoDbSettings": {
    "ConnectionString": "YOUR_MONGO_CONNECTION_STRING",
    "DatabaseName": "WeatherOnWheelsDb"
  }
}

3. Run the project

## API Endpoints

POST /api/place - Create a new place

GET /api/place - Retrieve all places

## Important Notes
The backend will automatically fetch coordinates based on the given address using Nominatim API.

Ensure MongoDB is running.
