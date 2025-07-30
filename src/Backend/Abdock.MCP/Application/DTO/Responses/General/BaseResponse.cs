using Application.DTO.Responses.General.Enums;

namespace Application.DTO.Responses.General;

public record BaseResponse<TResponse>
{
    protected BaseResponse()
    {
    }

    public required CustomStatusCode StatusCode { get; init; }
    public bool IsSuccess => StatusCode == CustomStatusCode.Ok;
    public TResponse? Response { get; private init; }
    public string? ErrorMessage { get; private init; }

    public static implicit operator BaseResponse<TResponse>(TResponse response)
    {
        return new BaseResponse<TResponse>
        {
            StatusCode = CustomStatusCode.Ok,
            Response = response
        };
    }

    public static implicit operator BaseResponse<TResponse>(CustomStatusCode statusCode)
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

    public static implicit operator BaseResponse(CustomStatusCode statusCode)
    {
        return new BaseResponse
        {
            StatusCode = statusCode
        };
    }
}