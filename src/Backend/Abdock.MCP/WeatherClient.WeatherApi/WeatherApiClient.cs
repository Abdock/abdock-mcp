using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeatherClient.Abstractions;
using WeatherClient.Abstractions.Exceptions;
using WeatherClient.Abstractions.Models;
using WeatherClient.WeatherApi.Mapping;
using WeatherClient.WeatherApi.Models.Current;
using WeatherClient.WeatherApi.Options;

namespace WeatherClient.WeatherApi;

public class WeatherApiClient : IWeatherClient
{
    private const string BaseUrl = "http://api.weatherapi.com/v1";
    private readonly IOptions<WeatherApiOptions> _options;
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherApiClient> _logger;

    public WeatherApiClient(IOptions<WeatherApiOptions> options, HttpClient httpClient, ILogger<WeatherApiClient> logger)
    {
        _options = options;
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<BaseResponse<Weather>> GeCurrentWeatherForAddressAsync(string query, CancellationToken cancellationToken = default)
    {
        using var message = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/current.json?key={_options.Value.ApiKey}&q={query}&aqi=no");
        using var response = await _httpClient.SendAsync(message, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return response.StatusCode;
        }

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        var weather = JsonSerializer.Deserialize<CurrentWeatherResponse>(json);
        if (weather is not null)
        {
            return weather.MapToWeather();
        }

        _logger.LogCritical("Unable to deserialize weather api response, JSON: {Json}", json);
        throw new UnableParseResultException("Unable to deserialize response");
    }
}