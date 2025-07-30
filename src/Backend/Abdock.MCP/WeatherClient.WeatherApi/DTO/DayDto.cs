using System.Text.Json.Serialization;

namespace WeatherClient.WeatherApi.DTO;

internal record DayDto
{
    [JsonPropertyName("maxtemp_c")]
    public required double MaxtempC { get; init; }
    
    [JsonPropertyName("maxtemp_f")]
    public required double MaxtempF { get; init; }
    
    [JsonPropertyName("mintemp_c")]
    public required double MintempC { get; init; }
    
    [JsonPropertyName("mintemp_f")]
    public required double MintempF { get; init; }
    
    [JsonPropertyName("avgtemp_c")]
    public required double AvgtempC { get; init; }
    
    [JsonPropertyName("avgtemp_f")]
    public required double AvgtempF { get; init; }
    
    [JsonPropertyName("maxwind_mph")]
    public required double MaxwindMph { get; init; }
    
    [JsonPropertyName("maxwind_kph")]
    public required double MaxwindKph { get; init; }
    
    [JsonPropertyName("totalprecip_mm")]
    public required double TotalprecipMm { get; init; }
    
    [JsonPropertyName("totalprecip_in")]
    public required double TotalprecipIn { get; init; }
    
    [JsonPropertyName("avgvis_km")]
    public required double AvgvisKm { get; init; }
    
    [JsonPropertyName("avgvis_miles")]
    public required double AvgvisMiles { get; init; }
    
    [JsonPropertyName("avghumidity")]
    public required double Avghumidity { get; init; }
    
    [JsonPropertyName("daily_will_it_rain")]
    public required int DailyWillItRain { get; init; }
    
    [JsonPropertyName("daily_chance_of_rain")]
    public required double DailyChanceOfRain { get; init; }
    
    [JsonPropertyName("daily_will_it_snow")]
    public required int DailyWillItSnow { get; init; }
    
    [JsonPropertyName("daily_chance_of_snow")]
    public required double DailyChanceOfSnow { get; init; }
    
    [JsonPropertyName("condition")]
    public required ConditionDto Condition { get; init; }
    
    [JsonPropertyName("uv")]
    public required int Uv { get; init; }
}