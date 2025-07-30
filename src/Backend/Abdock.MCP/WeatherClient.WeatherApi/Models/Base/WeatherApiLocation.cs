using System.Text.Json.Serialization;

namespace WeatherClient.WeatherApi.Models.Base;

internal record WeatherApiLocation
{
    [JsonPropertyName("name")]
    public required string City { get; init; }

    [JsonPropertyName("region")]
    public required string Region { get; init; }

    [JsonPropertyName("country")]
    public required string Country { get; init; }

    [JsonPropertyName("lon")]
    public required double Longitude { get; init; }

    [JsonPropertyName("lat")]
    public required double Latitude { get; init; }
}