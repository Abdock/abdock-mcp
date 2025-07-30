using UnitsNet;
using WeatherClient.Abstractions.Models;
using WeatherClient.WeatherApi.DTO;

namespace WeatherClient.WeatherApi.Mapping;

internal static class AstronomyMapping
{
    public static Astronomy MapToAstronomy(this AstroDto dto)
    {
        return new Astronomy
        {
            Sunrise = TimeOnly.TryParseExact(dto.Sunrise, "hh:mm tt", out var sunrise) ? sunrise : null,
            Sunset = TimeOnly.TryParseExact(dto.Sunset, "hh:mm tt", out var sunset) ? sunset : null,
            Moonrise = TimeOnly.TryParseExact(dto.Moonrise, "hh:mm tt", out var moonrise) ? moonrise : null,
            Moonset = TimeOnly.TryParseExact(dto.Moonset, "hh:mm tt", out var moonset) ? moonset : null,
            MoonPhase = dto.MoonPhase,
            MoonIllumination = Ratio.FromPercent(dto.MoonIllumination)
        };
    }
}