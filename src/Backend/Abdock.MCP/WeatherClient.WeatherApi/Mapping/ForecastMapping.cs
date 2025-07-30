using WeatherClient.Abstractions.Models;
using WeatherClient.WeatherApi.DTO;

namespace WeatherClient.WeatherApi.Mapping;

internal static class ForecastMapping
{
    public static WeatherForecastDay MapToWeatherForecastDay(this ForecastDayDto dto) => new()
    {
        Date = DateOnly.ParseExact(dto.Date, "yyyy-MM-dd"),
        Day = dto.Day.MapToDailyWeather(),
        Astronomy = dto.Astro.MapToAstronomy(),
        HourlyForecast = dto.Hour.Select(hour => hour.MapToHourlyWeather()).ToArray()
    };
}