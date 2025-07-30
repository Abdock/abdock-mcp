using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args)
    .ConfigureSecrets()
    .ConfigureSerilog()
    .ConfigureWeatherApi()
    .ConfigureCqrs()
    .ConfigureMcpServer();

var app = builder.Build();
app.UseCustomExceptionHandler();
app.UseSerilogContextEnrich();
app.MapOpenApi("/{documentName}.json");
app.MapMcp("/mcp");
app.Run();