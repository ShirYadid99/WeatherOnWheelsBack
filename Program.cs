using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WeatherOnWheels.Models;
using WeatherOnWheels.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// קבלת ההגדרות מ-appsettings.json
builder.Services.Configure<WeatherDatabaseSettings>(
    builder.Configuration.GetSection("WeatherDatabaseSettings")
);

// הוספת MongoClient לשירותים של DI
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<WeatherDatabaseSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// הוספת IMongoDatabase לשירותים של DI
builder.Services.AddSingleton(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<WeatherDatabaseSettings>>().Value;
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});


builder.Services.AddSingleton<PlaceService>();
//builder.Services.AddHttpClient<GeocodingService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});





builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapGet("/", () => "API is running!");
app.MapControllers();

app.Run();
