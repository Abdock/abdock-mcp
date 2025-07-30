using System.Text.Json.Serialization;

namespace WeatherClient.WeatherApi.DTO;

internal record CurrentDto
{
    [JsonPropertyName("last_updated_epoch")]
    public required long LastUpdatedEpoch { get; init; }
    
    [JsonPropertyName("last_updated")]
    public required string LastUpdated { get; init; }
    
    [JsonPropertyName("temp_c")]
    public required double TempC { get; init; }
    
    [JsonPropertyName("temp_f")]
    public required double TempF { get; init; }
    
    [JsonPropertyName("is_day")]
    public required int IsDay { get; init; }
    
    [JsonPropertyName("condition")]
    public required ConditionDto Condition { get; init; }
    
    [JsonPropertyName("wind_mph")]
    public required double WindMph { get; init; }
    
    [JsonPropertyName("wind_kph")]
    public required double WindKph { get; init; }
    
    [JsonPropertyName("wind_degree")]
    public required double WindDegree { get; init; }
    
    [JsonPropertyName("wind_dir")]
    public required string WindDir { get; init; }
    
    [JsonPropertyName("pressure_mb")]
    public required double PressureMb { get; init; }
    
    [JsonPropertyName("pressure_in")]
    public required double PressureIn { get; init; }
    
    [JsonPropertyName("precip_mm")]
    public required double PrecipMm { get; init; }
    
    [JsonPropertyName("precip_in")]
    public required double PrecipIn { get; init; }
    
    [JsonPropertyName("humidity")]
    public required double Humidity { get; init; }
    
    [JsonPropertyName("cloud")]
    public required double Cloud { get; init; }
    
    [JsonPropertyName("feelslike_c")]
    public required double FeelslikeC { get; init; }
    
    [JsonPropertyName("feelslike_f")]
    public required double FeelslikeF { get; init; }
    
    [JsonPropertyName("vis_km")]
    public required double VisKm { get; init; }
    
    [JsonPropertyName("vis_miles")]
    public required double VisMiles { get; init; }
    
    [JsonPropertyName("uv")]
    public required int Uv { get; init; }
    
    [JsonPropertyName("gust_mph")]
    public required double GustMph { get; init; }
    
    [JsonPropertyName("gust_kph")]
    public required double GustKph { get; init; }
}