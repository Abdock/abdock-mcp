using System.Text.Json.Serialization;
using WeatherClient.WeatherApi.Models.Base;

namespace WeatherClient.WeatherApi.Models.Current;

internal record CurrentWeatherResponse
{
    [JsonPropertyName("location")]
    public required WeatherApiLocation Location { get; init; }

    [JsonPropertyName("current")]
    public required WeatherApiWeather Weather { get; init; }
}