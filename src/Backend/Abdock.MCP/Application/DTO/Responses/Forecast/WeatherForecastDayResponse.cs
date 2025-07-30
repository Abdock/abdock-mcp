namespace Application.DTO.Responses.Forecast;

public record WeatherForecastDayResponse
{
    public required string Date { get; init; }
    public required DailyWeatherResponse Day { get; init; }
    public required AstronomyResponse Astronomy { get; init; }
    public required IReadOnlyList<HourlyWeatherResponse> HourlyForecast { get; init; }
}