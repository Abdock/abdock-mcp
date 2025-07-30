using UnitsNet;
using WeatherClient.Abstractions.Models;
using WeatherClient.WeatherApi.Models.Current;

namespace WeatherClient.WeatherApi.Mapping;

internal static class WeatherMapping
{
    public static Weather MapToWeather(this CurrentWeatherResponse weather) => new()
    {
        Location = weather.Location.MapToLocation(),
        Temperature = Temperature.FromDegreesFahrenheit(weather.Weather.TemperatureF),
        FeelsLike = Temperature.FromDegreesFahrenheit(weather.Weather.FeelsLikeF),
        WindSpeed = Speed.FromMetersPerHour(weather.Weather.WindSpeedMeterPerHour),
        Pressure = Pressure.FromMegabars(weather.Weather.PressureMb)
    };
}