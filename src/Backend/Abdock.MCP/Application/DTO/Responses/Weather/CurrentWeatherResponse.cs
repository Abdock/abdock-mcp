using Application.DTO.Responses.Location;
using UnitsNet;

namespace Application.DTO.Responses.Weather;

public record CurrentWeatherResponse
{
    public required LocationResponse Location { get; init; }
    public required Temperature Temperature { get; init; }
    public Temperature? FeelsLike { get; init; }
    public Speed? WindSpeed { get; init; }
    public Pressure? Pressure { get; init; }
}