using WeatherClient.Abstractions.Exceptions;
using WeatherClient.Abstractions.Models;

namespace WeatherClient.Abstractions;

public interface IWeatherClient
{
    /// <summary>
    /// Get weather for current time for given address
    /// </summary>
    /// <param name="query">Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="UnableParseResultException">Unable to parse API response</exception>
    /// <returns>Weather data for current time, for given address</returns>
    Task<BaseResponse<Weather>> GeCurrentWeatherAsync(string query, CancellationToken cancellationToken = default);
    Task<BaseResponse<IReadOnlyCollection<WeatherForecastDay>>> GetWeatherForecastAsync(string query, int days = 1, CancellationToken cancellationToken = default);
    Task<BaseResponse<IReadOnlyCollection<WeatherAlert>>> GetWeatherAlertsAsync(string query, int days = 1, CancellationToken cancellationToken = default);
}