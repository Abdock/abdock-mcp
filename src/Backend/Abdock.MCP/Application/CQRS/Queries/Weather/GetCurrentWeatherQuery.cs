using Application.DTO.Mappers;
using Application.DTO.Requests.Weather;
using Application.DTO.Responses.General;
using Application.DTO.Responses.Weather;
using Mediator;
using WeatherClient.Abstractions;

namespace Application.CQRS.Queries.Weather;

public class GetCurrentWeatherQuery : IQuery<BaseResponse<CurrentWeatherResponse>>
{
    public required GetCurrentWeatherRequest Request { get; init; }
}

public class GetCurrentWeatherQueryHandler : IQueryHandler<GetCurrentWeatherQuery, BaseResponse<CurrentWeatherResponse>>
{
    private readonly IWeatherClient _weatherClient;
    private readonly ResponseMapper _mapper;

    public GetCurrentWeatherQueryHandler(IWeatherClient weatherClient, ResponseMapper mapper)
    {
        _weatherClient = weatherClient;
        _mapper = mapper;
    }

    public async ValueTask<BaseResponse<CurrentWeatherResponse>> Handle(GetCurrentWeatherQuery query, CancellationToken cancellationToken)
    {
        var response = await _weatherClient.GeCurrentWeatherAsync(query.Request.Query, cancellationToken);
        if (response.IsSuccess)
        {
            return _mapper.MapToResponse(response.Response!);
        }

        if (response.Error is null)
        {
            return response.StatusCode.MapToCustomStatusCode();
        }

        var code = response.Error.ErrorType.MapToCustomStatusCode();
        return code;
    }
}