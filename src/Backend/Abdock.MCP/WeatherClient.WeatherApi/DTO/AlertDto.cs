using System.Text.Json.Serialization;

namespace WeatherClient.WeatherApi.DTO;

internal record AlertDto
{
    [JsonPropertyName("headline")]
    public required string Headline { get; init; }
    
    [JsonPropertyName("msgtype")]
    public string? Msgtype { get; init; }
    
    [JsonPropertyName("severity")]
    public string? Severity { get; init; }
    
    [JsonPropertyName("urgency")]
    public string? Urgency { get; init; }
    
    [JsonPropertyName("areas")]
    public string? Areas { get; init; }
    
    [JsonPropertyName("category")]
    public required string Category { get; init; }
    
    [JsonPropertyName("certainty")]
    public string? Certainty { get; init; }
    
    [JsonPropertyName("event")]
    public required string Event { get; init; }
    
    [JsonPropertyName("note")]
    public string? Note { get; init; }
    
    [JsonPropertyName("effective")]
    public required string Effective { get; init; }
    
    [JsonPropertyName("expires")]
    public required string Expires { get; init; }
    
    [JsonPropertyName("desc")]
    public required string Desc { get; init; }
    
    [JsonPropertyName("instruction")]
    public required string Instruction { get; init; }
}