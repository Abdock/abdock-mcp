using System.Text.Json.Serialization;

namespace WeatherClient.WeatherApi.DTO;

internal record ForecastDayDto
{
    [JsonPropertyName("date")]
    public required string Date { get; init; }
    
    [JsonPropertyName("date_epoch")]
    public required long DateEpoch { get; init; }
    
    [JsonPropertyName("day")]
    public required DayDto Day { get; init; }
    
    [JsonPropertyName("astro")]
    public required AstroDto Astro { get; init; }
    
    [JsonPropertyName("hour")]
    public required HourDto[] Hour { get; init; }
}