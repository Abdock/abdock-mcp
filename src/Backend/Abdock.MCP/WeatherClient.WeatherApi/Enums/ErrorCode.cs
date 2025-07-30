namespace WeatherClient.WeatherApi.Enums;

public enum ErrorCode
{
    ApiKeyNotProvided = 1002,
    QueryParameterNotProvided = 1003,
    InvalidApiRequestUrl = 1005,
    QueriedLocationWasNotFound = 1006,
    InvalidApiKey = 2006,
    ApiKeyHasExceededOfQuota = 2007,
    ApiKeyDisabled = 2008,
    ApiKeyDoesNotHaveAccess = 2009,
    InvalidJsonBody = 9000,
    JsonBodyContainsTooManyLocations = 9001,
    InternalApplicationError = 9999
}