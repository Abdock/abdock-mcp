using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherClient.Abstractions;
using WeatherClient.WeatherApi.Options;

namespace WeatherClient.WeatherApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWeatherApi(this IServiceCollection services, IConfiguration configuration, string sectionName = "WeatherApi")
    {
        var section = configuration.GetSection(sectionName);
        services.Configure<WeatherApiOptions>(section);
        services.AddSingleton<IWeatherClient, WeatherApiClient>();
        services.AddHttpClient<WeatherApiClient>();
        return services;
    }
}