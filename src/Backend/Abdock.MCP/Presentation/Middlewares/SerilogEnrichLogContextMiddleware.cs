using Presentation.Constants;
using Serilog.Context;

namespace Presentation.Middlewares;

public class SerilogEnrichLogContextMiddleware
{
    private readonly RequestDelegate _next;

    public SerilogEnrichLogContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        using var correlationId = LogContext.PushProperty(LoggingConstants.CorrelationIdKey, httpContext.TraceIdentifier);
        await _next(httpContext);
    }
}