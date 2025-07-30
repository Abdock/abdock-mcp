namespace WeatherClient.Abstractions.Models;

public record WeatherForecastDay
{
    public required DateOnly? Date { get; init; }
    public required DailyWeather Day { get; init; }
    public required Astronomy Astronomy { get; init; }
    public required IReadOnlyList<HourlyWeather> HourlyForecast { get; init; }
}