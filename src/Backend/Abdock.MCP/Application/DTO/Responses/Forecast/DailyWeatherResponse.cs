namespace Application.DTO.Responses.Forecast;

public record DailyWeatherResponse
{
    public required string MaxTemperature { get; init; }
    public required string MinTemperature { get; init; }
    public required string AverageTemperature { get; init; }
    public required string MaxWindSpeed { get; init; }
    public required string TotalPrecipitation { get; init; }
    public required string AverageVisibility { get; init; }
    public required string AverageHumidity { get; init; }
    public required bool WillItRain { get; init; }
    public required string ChanceOfRain { get; init; }
    public required bool WillItSnow { get; init; }
    public required string ChanceOfSnow { get; init; }
    public required string ConditionText { get; init; }
    public required string ConditionIcon { get; init; }
    public required int ConditionCode { get; init; }
    public required double UvIndex { get; init; }
}