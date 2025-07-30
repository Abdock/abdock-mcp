using Application.DTO.Mappers;
using Application.DTO.Requests.Forecast;
using Application.DTO.Responses.Forecast;
using Application.DTO.Responses.General;
using Mediator;
using WeatherClient.Abstractions;

namespace Application.CQRS.Queries.Forecast;

public class GetWeatherForecastQuery : IQuery<BaseResponse<IReadOnlyCollection<WeatherForecastDayResponse>>>
{
    public required GetWeatherForecastRequest Request { get; init; }
}

public class GetWeatherForecastQueryHandler : IQueryHandler<GetWeatherForecastQuery, BaseResponse<IReadOnlyCollection<WeatherForecastDayResponse>>>
{
    private readonly IWeatherClient _weatherClient;
    private readonly ResponseMapper _mapper;

    public GetWeatherForecastQueryHandler(IWeatherClient weatherClient, ResponseMapper mapper)
    {
        _weatherClient = weatherClient;
        _mapper = mapper;
    }

    public async ValueTask<BaseResponse<IReadOnlyCollection<WeatherForecastDayResponse>>> Handle(GetWeatherForecastQuery query, CancellationToken cancellationToken)
    {
        var response = await _weatherClient.GetWeatherForecastAsync(query.Request.Query, query.Request.Days, cancellationToken);
        if (response.IsSuccess)
        {
            return response.Response!.Select(forecast => _mapper.MapToResponse(forecast)).ToArray();
        }

        if (response.Error is null)
        {
            return response.StatusCode.MapToCustomStatusCode();
        }

        var code = response.Error.ErrorType.MapToCustomStatusCode();
        return code;
    }
}