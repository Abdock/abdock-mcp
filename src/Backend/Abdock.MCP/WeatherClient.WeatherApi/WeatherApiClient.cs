using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeatherClient.Abstractions;
using WeatherClient.Abstractions.Exceptions;
using WeatherClient.Abstractions.Models;
using WeatherClient.WeatherApi.DTO;
using WeatherClient.WeatherApi.Mapping;
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

    public async Task<ApiResponse<Weather>> GeCurrentWeatherAsync(string query, CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/current.json?key={_options.Value.ApiKey}&q={Uri.EscapeDataString(query)}&aqi=no";
        using var message = new HttpRequestMessage(HttpMethod.Get, url);
        using var response = await _httpClient.SendAsync(message, cancellationToken);
        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            var weather = DeserializeOrThrow<CurrentWeatherResponse>(json);
            return weather.MapToWeather();
        }

        var dto = DeserializeOrThrow<ErrorDto>(json);
        return dto.MapToErrorResponse();
    }

    public async Task<ApiResponse<IReadOnlyCollection<WeatherForecastDay>>> GetWeatherForecastAsync(string query, int days = 1, CancellationToken cancellationToken = default)
    {
        if (days is < 1 or > 14)
        {
            throw new ArgumentOutOfRangeException(nameof(days), "Days must be between 1 and 14");
        }

        var url = $"{BaseUrl}/forecast.json?key={_options.Value.ApiKey}&q={Uri.EscapeDataString(query)}&days={days}&aqi=no&alerts=no";
        using var message = new HttpRequestMessage(HttpMethod.Get, url);
        using var response = await _httpClient.SendAsync(message, cancellationToken);
        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            var forecastResponse = DeserializeOrThrow<ForecastWeatherResponse>(json);
            return forecastResponse.Forecast.Forecastday.Select(forecast => forecast.MapToWeatherForecastDay()).ToArray();
        }

        var dto = DeserializeOrThrow<ErrorDto>(json);
        return dto.MapToErrorResponse();
    }

    public async Task<ApiResponse<IReadOnlyCollection<WeatherAlert>>> GetWeatherAlertsAsync(string query, int days = 1, CancellationToken cancellationToken = default)
    {
        if (days is < 1 or > 14)
        {
            throw new ArgumentOutOfRangeException(nameof(days), "Days must be between 1 and 14");
        }

        var url = $"{BaseUrl}/forecast.json?key={_options.Value.ApiKey}&q={Uri.EscapeDataString(query)}&days={days}&aqi=no&alerts=yes";
        using var message = new HttpRequestMessage(HttpMethod.Get, url);
        using var response = await _httpClient.SendAsync(message, cancellationToken);
        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            var forecastResponse = DeserializeOrThrow<ForecastWeatherResponse>(json);
            return forecastResponse.Alerts?.Alert.Select(alert => alert.MapToWeatherAlert()).ToArray() ?? [];
        }

        var dto = DeserializeOrThrow<ErrorDto>(json);
        return dto.MapToErrorResponse();
    }

    private TParsed DeserializeOrThrow<TParsed>(string json)
    {
        var parsed = JsonSerializer.Deserialize<TParsed>(json);
        if (parsed is not null)
        {
            return parsed;
        }

        _logger.LogCritical("Unable to deserialize API response, JSON: {Json}", json);
        throw new UnableParseResultException("Unable to deserialize response");
    }
}