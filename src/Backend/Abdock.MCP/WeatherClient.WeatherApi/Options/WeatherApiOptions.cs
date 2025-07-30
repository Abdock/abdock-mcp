namespace WeatherClient.WeatherApi.Options;

public record WeatherApiOptions
{
    public required string ApiKey { get; init; }
}