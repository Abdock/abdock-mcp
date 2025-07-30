using Application.DTO.Mappers;
using Application.DTO.Requests.Alert;
using Application.DTO.Responses.Alert;
using Application.DTO.Responses.General;
using Mediator;
using WeatherClient.Abstractions;

namespace Application.CQRS.Queries.Alerts;

public class GetWeatherForecastAlertsQuery : IQuery<BaseResponse<IReadOnlyCollection<WeatherAlertResponse>>>
{
    public required GetWeatherForecastAlertsRequest Request { get; init; }
}

public class GetWeatherForecastAlertsQueryHandler : IQueryHandler<GetWeatherForecastAlertsQuery, BaseResponse<IReadOnlyCollection<WeatherAlertResponse>>>
{
    private readonly IWeatherClient _weatherClient;
    private readonly ResponseMapper _mapper;

    public GetWeatherForecastAlertsQueryHandler(IWeatherClient weatherClient, ResponseMapper mapper)
    {
        _weatherClient = weatherClient;
        _mapper = mapper;
    }

    public async ValueTask<BaseResponse<IReadOnlyCollection<WeatherAlertResponse>>> Handle(GetWeatherForecastAlertsQuery query, CancellationToken cancellationToken)
    {
        var response = await _weatherClient.GetWeatherAlertsAsync(query.Request.Query, query.Request.Days, cancellationToken);
        if (response.IsSuccess)
        {
            return response.Response!.Select(alert => _mapper.MapToResponse(alert)).ToArray();
        }

        if (response.Error is null)
        {
            return response.StatusCode.MapToCustomStatusCode();
        }

        var code = response.Error.ErrorType.MapToCustomStatusCode();
        return code;
    }
}