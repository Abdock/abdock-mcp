using System.Text.Json.Serialization;

namespace WeatherClient.WeatherApi.DTO;

internal record ForecastDto
{
    [JsonPropertyName("forecastday")]
    public required ForecastDayDto[] Forecastday { get; init; }
}