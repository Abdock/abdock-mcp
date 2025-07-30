using UnitsNet;

namespace WeatherClient.Abstractions.Models;

public record DailyWeather
{
    public required Temperature MaxTemperature { get; init; }
    public required Temperature MinTemperature { get; init; }
    public required Temperature AverageTemperature { get; init; }
    public required Speed MaxWindSpeed { get; init; }
    public required Length TotalPrecipitation { get; init; }
    public required Length AverageVisibility { get; init; }
    public required RelativeHumidity AverageHumidity { get; init; }
    public required bool WillItRain { get; init; }
    public required Ratio ChanceOfRain { get; init; }
    public required bool WillItSnow { get; init; }
    public required Ratio ChanceOfSnow { get; init; }
    public required string ConditionText { get; init; }
    public required string ConditionIcon { get; init; }
    public required int ConditionCode { get; init; }
    public required double UvIndex { get; init; }
}