using UnitsNet;

namespace Application.DTO.Responses.Forecast;

public record AstronomyResponse
{
    public required TimeOnly Sunrise { get; init; }
    public required TimeOnly Sunset { get; init; }
    public required TimeOnly Moonrise { get; init; }
    public required TimeOnly Moonset { get; init; }
    public required string MoonPhase { get; init; }
    public required Ratio MoonIllumination { get; init; }
}