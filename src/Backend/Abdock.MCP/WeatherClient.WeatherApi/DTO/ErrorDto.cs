using System.Text.Json.Serialization;
using WeatherClient.WeatherApi.Enums;

namespace WeatherClient.WeatherApi.DTO;

internal record ErrorDto
{
    [JsonPropertyName("code")]
    public required ErrorCode Code { get; init; }

    [JsonPropertyName("message")]
    public required string Message { get; init; }
}