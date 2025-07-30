using Application.DTO.Responses.Alert;
using Application.DTO.Responses.Forecast;
using Application.DTO.Responses.Weather;
using Riok.Mapperly.Abstractions;
using WeatherClient.Abstractions.Models;

namespace Application.DTO.Mappers;

[Mapper]
public partial class ResponseMapper
{
    public partial CurrentWeatherResponse MapToResponse(Weather weather);
    public partial WeatherForecastDayResponse MapToResponse(WeatherForecastDay forecastDay);
    public partial WeatherAlertResponse MapToResponse(WeatherAlert weather);
}