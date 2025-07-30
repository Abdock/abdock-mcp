using System.ComponentModel;
using Application.CQRS.Queries.Alerts;
using Application.CQRS.Queries.Forecast;
using Application.CQRS.Queries.Weather;
using Application.DTO.Requests.Alert;
using Application.DTO.Requests.Forecast;
using Application.DTO.Requests.Weather;
using Application.DTO.Responses.Alert;
using Application.DTO.Responses.Forecast;
using Application.DTO.Responses.General;
using Application.DTO.Responses.Weather;
using Mediator;
using ModelContextProtocol.Server;

namespace Presentation.Tools;

public class WeatherTools
{
    private readonly IMediator _mediator;

    public WeatherTools(IMediator mediator)
    {
        _mediator = mediator;
    }

    [McpServerTool(Name = "get_current_weather", Title = "Get current weather")]
    [Description("Get current weather data for current date time")]
    public async Task<BaseResponse<CurrentWeatherResponse>> GetCurrentWeather(
        [Description("Query string, could be: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name")]
        string queryString)
    {
        var query = new GetCurrentWeatherQuery
        {
            Request = new GetCurrentWeatherRequest(queryString)
        };
        var response = await _mediator.Send(query).ConfigureAwait(false);
        return response;
    }

    [McpServerTool(Name = "get_weather_forecast", Title = "Get weather forecast")]
    [Description("Get weather forecast data for given query and days")]
    public async Task<BaseResponse<IReadOnlyCollection<WeatherForecastDayResponse>>> GetWeatherForecast(
        [Description("Query string, could be: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name")]
        string queryString,
        [Description("Number of days of forecast required. Value ranges between 1 and 14.")]
        int days)
    {
        var query = new GetWeatherForecastQuery
        {
            Request = new GetWeatherForecastRequest(queryString, days)
        };
        var response = await _mediator.Send(query).ConfigureAwait(false);
        return response;
    }

    [McpServerTool(Name = "get_weather_forecast_alerts", Title = "Get weather forecast alerts")]
    [Description("Get weather forecast alerts data for given query and days")]
    public async Task<BaseResponse<IReadOnlyCollection<WeatherAlertResponse>>> GetWeatherForecastAlerts(
        [Description("Query string, could be: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name")]
        string queryString,
        [Description("Number of days of forecast required. Value ranges between 1 and 14.")]
        int days)
    {
        var query = new GetWeatherForecastAlertsQuery()
        {
            Request = new GetWeatherForecastAlertsRequest(queryString, days)
        };
        var response = await _mediator.Send(query).ConfigureAwait(false);
        return response;
    }
}