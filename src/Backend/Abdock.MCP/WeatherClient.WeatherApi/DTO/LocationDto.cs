using System.Text.Json.Serialization;

namespace WeatherClient.WeatherApi.DTO;

internal record LocationDto
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    
    [JsonPropertyName("region")]
    public required string Region { get; init; }
    
    [JsonPropertyName("country")]
    public required string Country { get; init; }
    
    [JsonPropertyName("lat")]
    public required double Lat { get; init; }
    
    [JsonPropertyName("lon")]
    public required double Lon { get; init; }
    
    [JsonPropertyName("tz_id")]
    public required string TzId { get; init; }
    
    [JsonPropertyName("localtime_epoch")]
    public required long LocaltimeEpoch { get; init; }
    
    [JsonPropertyName("localtime")]
    public required string Localtime { get; init; }
}