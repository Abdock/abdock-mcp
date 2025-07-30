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

    /// <summary>
    /// Get weather forecast for a given location
    /// </summary>
    /// <param name="query">Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name</param>
    /// <param name="days">Number of days of weather forecast.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="ArgumentOutOfRangeException">Days parameter must be in range</exception>
    /// <exception cref="UnableParseResultException">Unable to parse API response</exception>
    /// <returns>Weather forecast data including current conditions and daily forecasts for the specified location</returns>
    Task<BaseResponse<IReadOnlyCollection<WeatherForecastDay>>> GetWeatherForecastAsync(string query, int days = 1, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get weather alerts for a given location
    /// </summary>
    /// <param name="query">Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name</param>
    /// <param name="days">Number of days of weather forecast alerts. Value ranges from 1 to 14</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="ArgumentOutOfRangeException">Days parameter must be between 1 and 14</exception>
    /// <exception cref="UnableParseResultException">Unable to parse API response</exception>
    /// <returns>Weather alerts data for the specified location</returns>
    Task<BaseResponse<IReadOnlyCollection<WeatherAlert>>> GetWeatherAlertsAsync(string query, int days = 1, CancellationToken cancellationToken = default);
}