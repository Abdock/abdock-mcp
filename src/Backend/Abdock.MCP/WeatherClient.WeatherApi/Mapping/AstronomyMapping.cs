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
            Sunrise = TimeOnly.ParseExact(dto.Sunrise, "hh:mm tt"),
            Sunset = TimeOnly.ParseExact(dto.Sunset, "hh:mm tt"),
            Moonrise = TimeOnly.ParseExact(dto.Moonrise, "hh:mm tt"),
            Moonset = TimeOnly.ParseExact(dto.Moonset, "hh:mm tt"),
            MoonPhase = dto.MoonPhase,
            MoonIllumination = Ratio.FromPercent(double.Parse(dto.MoonIllumination))
        };
    }
}