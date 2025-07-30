using UnitsNet;

namespace Application.DTO.Responses.Forecast;

public record HourlyWeatherResponse
{
    public required DateTime Time { get; init; }
    public required Temperature Temperature { get; init; }
    public required Temperature FeelsLike { get; init; }
    public required bool IsDay { get; init; }
    public required string ConditionText { get; init; }
    public required string ConditionIcon { get; init; }
    public required int ConditionCode { get; init; }
    public required Speed WindSpeed { get; init; }
    public required Angle WindDirection { get; init; }
    public required string WindDirectionCompass { get; init; }
    public required Pressure Pressure { get; init; }
    public required Length Precipitation { get; init; }
    public required RelativeHumidity Humidity { get; init; }
    public required Ratio CloudCover { get; init; }
    public required Temperature WindChill { get; init; }
    public required Temperature HeatIndex { get; init; }
    public required Temperature DewPoint { get; init; }
    public required bool WillItRain { get; init; }
    public required Ratio ChanceOfRain { get; init; }
    public required bool WillItSnow { get; init; }
    public required Ratio ChanceOfSnow { get; init; }
    public required Length Visibility { get; init; }
    public required Speed GustSpeed { get; init; }
    public required int UvIndex { get; init; }
}