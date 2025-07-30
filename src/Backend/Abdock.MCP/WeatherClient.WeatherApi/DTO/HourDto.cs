using System.Text.Json.Serialization;

namespace WeatherClient.WeatherApi.DTO;

internal record HourDto
{
    [JsonPropertyName("time_epoch")]
    public required long TimeEpoch { get; init; }
    
    [JsonPropertyName("time")]
    public required string Time { get; init; }
    
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
    
    [JsonPropertyName("windchill_c")]
    public required double WindchillC { get; init; }
    
    [JsonPropertyName("windchill_f")]
    public required double WindchillF { get; init; }
    
    [JsonPropertyName("heatindex_c")]
    public required double HeatindexC { get; init; }
    
    [JsonPropertyName("heatindex_f")]
    public required double HeatindexF { get; init; }
    
    [JsonPropertyName("dewpoint_c")]
    public required double DewpointC { get; init; }
    
    [JsonPropertyName("dewpoint_f")]
    public required double DewpointF { get; init; }
    
    [JsonPropertyName("will_it_rain")]
    public required int WillItRain { get; init; }
    
    [JsonPropertyName("chance_of_rain")]
    public required double ChanceOfRain { get; init; }
    
    [JsonPropertyName("will_it_snow")]
    public required int WillItSnow { get; init; }
    
    [JsonPropertyName("chance_of_snow")]
    public required double ChanceOfSnow { get; init; }
    
    [JsonPropertyName("vis_km")]
    public required double VisKm { get; init; }
    
    [JsonPropertyName("vis_miles")]
    public required double VisMiles { get; init; }
    
    [JsonPropertyName("gust_mph")]
    public required double GustMph { get; init; }
    
    [JsonPropertyName("gust_kph")]
    public required double GustKph { get; init; }
    
    [JsonPropertyName("uv")]
    public required double Uv { get; init; }
}