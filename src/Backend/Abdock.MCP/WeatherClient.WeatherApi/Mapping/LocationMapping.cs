using WeatherClient.Abstractions.Models;
using WeatherClient.WeatherApi.DTO;

namespace WeatherClient.WeatherApi.Mapping;

internal static class LocationMapping
{
    public static Location MapToLocation(this LocationDto location) => new()
    {
        City = location.Name,
        Country = location.Country,
        Latitude = location.Lat,
        Longitude = location.Lon,
        Region = location.Region,
    };
}