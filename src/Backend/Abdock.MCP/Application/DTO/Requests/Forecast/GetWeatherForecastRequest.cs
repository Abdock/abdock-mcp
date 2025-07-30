namespace Application.DTO.Requests.Forecast;

public record GetWeatherForecastRequest(string Query, int Days);