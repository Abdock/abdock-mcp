using Application.DTO.Responses.Location;

namespace Application.DTO.Responses.Weather;

public record CurrentWeatherResponse
{
    public required LocationResponse Location { get; init; }
    public required string Temperature { get; init; }
    public string? FeelsLike { get; init; }
    public string? WindSpeed { get; init; }
    public string? Pressure { get; init; }
}