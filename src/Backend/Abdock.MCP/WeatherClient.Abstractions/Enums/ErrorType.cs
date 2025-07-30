namespace WeatherClient.Abstractions.Enums;

public enum ErrorType
{
    InvalidRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    ApiError = 500
}