namespace WeatherClient.WeatherApi.Options;

public record WeatherApiOptions
{
    public string ApiKey { get; set; } = string.Empty;
}