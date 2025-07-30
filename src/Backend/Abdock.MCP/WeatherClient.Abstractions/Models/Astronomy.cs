using UnitsNet;

namespace WeatherClient.Abstractions.Models;

public record Astronomy
{
    public required TimeOnly Sunrise { get; init; }
    public required TimeOnly Sunset { get; init; }
    public required TimeOnly Moonrise { get; init; }
    public required TimeOnly Moonset { get; init; }
    public required string MoonPhase { get; init; }
    public required Ratio MoonIllumination { get; init; }
}