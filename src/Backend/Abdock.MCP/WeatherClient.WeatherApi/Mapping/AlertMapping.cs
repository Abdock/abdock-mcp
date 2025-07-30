using WeatherClient.Abstractions.Models;
using WeatherClient.WeatherApi.DTO;

namespace WeatherClient.WeatherApi.Mapping;

internal static class AlertMapping
{
    public static WeatherAlert MapToWeatherAlert(this AlertDto dto)
    {
        return new WeatherAlert
        {
            Headline = dto.Headline,
            MessageType = dto.Msgtype,
            Severity = dto.Severity,
            Urgency = dto.Urgency,
            Areas = dto.Areas,
            Category = dto.Category,
            Certainty = dto.Certainty,
            Event = dto.Event,
            Note = dto.Note,
            Effective = DateTime.Parse(dto.Effective),
            Expires = DateTime.Parse(dto.Expires),
            Description = dto.Desc,
            Instruction = dto.Instruction
        };
    }
}