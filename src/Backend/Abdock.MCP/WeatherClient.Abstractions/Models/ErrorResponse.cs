using WeatherClient.Abstractions.Enums;

namespace WeatherClient.Abstractions.Models;

public record ErrorResponse
{
    public required ErrorType ErrorType { get; init; }
    public required string Message { get; init; }
}