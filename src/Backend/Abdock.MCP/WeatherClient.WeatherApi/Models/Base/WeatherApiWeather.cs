using System.Text.Json.Serialization;

namespace WeatherClient.WeatherApi.Models.Base;

internal record WeatherApiWeather
{
    [JsonPropertyName("temp_f")]
    public required double TemperatureF { get; init; }

    [JsonPropertyName("wind_mph")]
    public required double WindSpeedMeterPerHour { get; init; }

    [JsonPropertyName("pressure_mb")]
    public required double PressureMb { get; init; }

    [JsonPropertyName("feelslike_f")]
    public required double FeelsLikeF { get; init; }
}