using System.Text.Json.Serialization;

namespace WeatherClient.WeatherApi.DTO;

internal record CurrentWeatherResponse
{
    [JsonPropertyName("location")]
    public required LocationDto Location { get; init; }
    
    [JsonPropertyName("current")]
    public required CurrentDto Current { get; init; }
}
