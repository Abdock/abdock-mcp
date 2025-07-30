namespace Application.DTO.Requests.Alert;

public class GetWeatherForecastAlertsRequest
{
    public required string Query { get; init; }
    public required int Days { get; init; }
}