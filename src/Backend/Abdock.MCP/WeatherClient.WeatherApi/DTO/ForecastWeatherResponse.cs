using System.Text.Json.Serialization;

namespace WeatherClient.WeatherApi.DTO;

internal record ForecastWeatherResponse
{
    [JsonPropertyName("location")]
    public required LocationDto Location { get; init; }
    
    [JsonPropertyName("current")]
    public required CurrentDto Current { get; init; }
    
    [JsonPropertyName("forecast")]
    public required ForecastDto Forecast { get; init; }
    
    [JsonPropertyName("alerts")]
    public AlertsDto? Alerts { get; init; }
}