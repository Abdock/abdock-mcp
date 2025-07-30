using System.Net;

namespace WeatherClient.Abstractions.Models;

public record ApiResponse<TResponse>
{
    protected ApiResponse()
    {
    }

    public required HttpStatusCode StatusCode { get; init; }
    public bool IsSuccess => StatusCode == HttpStatusCode.OK;
    public TResponse? Response { get; private init; }
    public ErrorResponse? Error { get; private init; }

    public static implicit operator ApiResponse<TResponse>(TResponse response)
    {
        return new ApiResponse<TResponse>
        {
            StatusCode = HttpStatusCode.OK,
            Response = response
        };
    }

    public static implicit operator ApiResponse<TResponse>(HttpStatusCode statusCode)
    {
        return new ApiResponse<TResponse>
        {
            StatusCode = statusCode
        };
    }

    public static implicit operator ApiResponse<TResponse>(ErrorResponse error)
    {
        return new ApiResponse<TResponse>
        {
            StatusCode = (HttpStatusCode) error.ErrorType,
            Error = error
        };
    }
}

public record ApiResponse : ApiResponse<object>
{
    private ApiResponse()
    {
    }

    public static implicit operator ApiResponse(HttpStatusCode statusCode)
    {
        return new ApiResponse
        {
            StatusCode = statusCode
        };
    }
}