using UnitsNet;

namespace WeatherClient.Abstractions.Models;

public record Weather
{
    public required Location Location { get; init; }
    public required Temperature Temperature { get; init; }
    public Temperature? FeelsLike { get; init; }
    public Speed? WindSpeed { get; init; }
    public Pressure? Pressure { get; init; }
}