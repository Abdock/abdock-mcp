using System.Net;
using Application.DTO.Responses.General.Enums;
using WeatherClient.Abstractions.Enums;

namespace Application.DTO.Mappers;

public static class ErrorTypeMapping
{
    public static CustomStatusCode MapToCustomStatusCode(this ErrorType errorType) => errorType switch
    {
        ErrorType.Forbidden or ErrorType.Unauthorized or ErrorType.ApiError => CustomStatusCode.InternalError,
        ErrorType.InvalidRequest => CustomStatusCode.InvalidRequest,
        _ => CustomStatusCode.Unknown
    };

    public static CustomStatusCode MapToCustomStatusCode(this HttpStatusCode errorType) => errorType switch
    {
        HttpStatusCode.Forbidden or HttpStatusCode.Unauthorized => CustomStatusCode.InternalError,
        HttpStatusCode.BadRequest => CustomStatusCode.InvalidRequest,
        _ => CustomStatusCode.Unknown
    };
}