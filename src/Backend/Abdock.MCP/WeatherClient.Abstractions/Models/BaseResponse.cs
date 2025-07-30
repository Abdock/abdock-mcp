using System.Net;

namespace WeatherClient.Abstractions.Models;

public record BaseResponse<TResponse>
{
    protected BaseResponse()
    {
    }

    public required HttpStatusCode StatusCode { get; init; }
    public bool IsSuccess => StatusCode == HttpStatusCode.OK;
    public TResponse? Response { get; private init; }

    public static implicit operator BaseResponse<TResponse>(TResponse response)
    {
        return new BaseResponse<TResponse>
        {
            StatusCode = HttpStatusCode.OK,
            Response = response
        };
    }

    public static implicit operator BaseResponse<TResponse>(HttpStatusCode statusCode)
    {
        return new BaseResponse<TResponse>
        {
            StatusCode = statusCode
        };
    }
}

public record BaseResponse : BaseResponse<object>
{
    private BaseResponse()
    {
    }

    public static implicit operator BaseResponse(HttpStatusCode statusCode)
    {
        return new BaseResponse
        {
            StatusCode = statusCode
        };
    }
}