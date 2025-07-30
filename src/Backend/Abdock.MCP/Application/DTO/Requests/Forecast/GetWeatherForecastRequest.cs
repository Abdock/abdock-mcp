namespace Application.DTO.Requests.Forecast;

public record GetWeatherForecastRequest
{
    public required string Query { get; init; }
    public required int Days { get; init; }
}