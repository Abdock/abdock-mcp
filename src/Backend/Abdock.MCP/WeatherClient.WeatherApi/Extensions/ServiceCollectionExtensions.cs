using Microsoft.Extensions.DependencyInjection;
using WeatherClient.Abstractions;
using WeatherClient.WeatherApi.Options;

namespace WeatherClient.WeatherApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWeatherApi(this IServiceCollection services, Action<WeatherApiOptions> configureOptions)
    {
        services.Configure(configureOptions);
        services.AddSingleton<IWeatherClient, WeatherApiClient>();
        services.AddHttpClient<WeatherApiClient>();
        return services;
    }
}