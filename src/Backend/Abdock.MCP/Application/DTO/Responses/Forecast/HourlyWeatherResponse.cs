namespace Application.DTO.Responses.Forecast;

public record HourlyWeatherResponse
{
    public required DateTime Time { get; init; }
    public required string Temperature { get; init; }
    public required string FeelsLike { get; init; }
    public required bool IsDay { get; init; }
    public required string ConditionText { get; init; }
    public required string ConditionIcon { get; init; }
    public required int ConditionCode { get; init; }
    public required string WindSpeed { get; init; }
    public required string WindDirection { get; init; }
    public required string WindDirectionCompass { get; init; }
    public required string Pressure { get; init; }
    public required string Precipitation { get; init; }
    public required string Humidity { get; init; }
    public required string CloudCover { get; init; }
    public required string WindChill { get; init; }
    public required string HeatIndex { get; init; }
    public required string DewPoint { get; init; }
    public required bool WillItRain { get; init; }
    public required string ChanceOfRain { get; init; }
    public required bool WillItSnow { get; init; }
    public required string ChanceOfSnow { get; init; }
    public required string Visibility { get; init; }
    public required string GustSpeed { get; init; }
    public required double UvIndex { get; init; }
}