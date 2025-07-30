using WeatherClient.Abstractions.Enums;
using WeatherClient.Abstractions.Models;
using WeatherClient.WeatherApi.DTO;
using WeatherClient.WeatherApi.Enums;

namespace WeatherClient.WeatherApi.Mapping;

internal static class ErrorMapping
{
    public static ErrorType MapToErrorType(this ErrorCode code) => code switch
    {
        ErrorCode.QueryParameterNotProvided or ErrorCode.InvalidApiRequestUrl or ErrorCode.QueriedLocationWasNotFound or ErrorCode.JsonBodyContainsTooManyLocations
            or ErrorCode.InvalidJsonBody => ErrorType.InvalidRequest,
        ErrorCode.ApiKeyNotProvided or ErrorCode.InvalidApiKey => ErrorType.Unauthorized,
        ErrorCode.ApiKeyDisabled or ErrorCode.ApiKeyHasExceededOfQuota or ErrorCode.ApiKeyDoesNotHaveAccess => ErrorType.Forbidden,
        _ => ErrorType.ApiError
    };

    public static ErrorResponse MapToErrorResponse(this ErrorDto dto) => new()
    {
        ErrorType = dto.Code.MapToErrorType(),
        Message = dto.Message
    };
}