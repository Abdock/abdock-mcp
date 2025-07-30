using System.Text.Json.Serialization;

namespace WeatherClient.WeatherApi.DTO;

internal record AlertsDto
{
    [JsonPropertyName("alert")]
    public required AlertDto[] Alert { get; init; }
}