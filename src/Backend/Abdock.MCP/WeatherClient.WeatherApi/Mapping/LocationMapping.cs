using WeatherClient.Abstractions.Models;
using WeatherClient.WeatherApi.Models.Base;

namespace WeatherClient.WeatherApi.Mapping;

internal static class LocationMapping
{
    public static Location MapToLocation(this WeatherApiLocation location) => new()
    {
        City = location.City,
        Country = location.Country,
        Latitude = location.Latitude,
        Longitude = location.Longitude,
        Region = location.Region,
    };
}