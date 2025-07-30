using Application.DTO.Mappers;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi;
using Presentation.OpenApi;
using Presentation.Tools;
using Serilog;
using Serilog.Events;
using WeatherClient.WeatherApi.Extensions;

namespace Presentation.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureSecrets(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddUserSecrets<Program>();
        return builder;
    }

    public static WebApplicationBuilder ConfigureWeatherApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddWeatherApi(options => options.ApiKey = builder.Configuration["WeatherApi:ApiKey"]!);
        return builder;
    }

    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        const string logMessageTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {EnvironmentName} {CorrelationId} {Level:u3}] IP: {ClientIp} {Operation} {Message:lj}{NewLine}{Exception}";
        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentName()
            .Enrich.WithClientIp()
            .WriteTo
            .Async(configuration =>
            {
                configuration.Console(restrictedToMinimumLevel: LogEventLevel.Information, outputTemplate: logMessageTemplate);
                configuration.File("logs/logs.log", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Information, outputTemplate: logMessageTemplate);
            })
            .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);
        builder.Services.AddHttpLogging(options =>
        {
            options.LoggingFields = HttpLoggingFields.RequestHeaders;
        });
        return builder;
    }

    public static WebApplicationBuilder ConfigureCqrs(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ResponseMapper>();
        builder.Services.AddMediator();
        return builder;
    }

    public static WebApplicationBuilder ConfigureMcpServer(this WebApplicationBuilder builder)
    {
        builder.Services.AddMcpServer()
            .WithHttpTransport(options => options.Stateless = true)
            .WithTools<WeatherTools>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddOpenApi("swagger", options =>
        {
            options.OpenApiVersion = OpenApiSpecVersion.OpenApi2_0;
            options.AddDocumentTransformer<McpDocumentTransformer>();
        });
        builder.Services.AddOpenApi("openapi", options =>
        {
            options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
            options.AddDocumentTransformer<McpDocumentTransformer>();
        });
        return builder;
    }
}