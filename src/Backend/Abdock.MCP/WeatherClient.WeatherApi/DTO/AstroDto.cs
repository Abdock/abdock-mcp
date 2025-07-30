using System.Text.Json.Serialization;

namespace WeatherClient.WeatherApi.DTO;

internal record AstroDto
{
    [JsonPropertyName("sunrise")]
    public required string Sunrise { get; init; }
    
    [JsonPropertyName("sunset")]
    public required string Sunset { get; init; }
    
    [JsonPropertyName("moonrise")]
    public required string Moonrise { get; init; }
    
    [JsonPropertyName("moonset")]
    public required string Moonset { get; init; }
    
    [JsonPropertyName("moon_phase")]
    public required string MoonPhase { get; init; }
    
    [JsonPropertyName("moon_illumination")]
    public required string MoonIllumination { get; init; }
}