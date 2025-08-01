﻿using Presentation.Middlewares;

namespace Presentation.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseCustomExceptionHandler(this WebApplication app)
    {
        app.UseMiddleware<CustomExceptionHandlerMiddleware>();
        return app;
    }

    public static WebApplication UseSerilogContextEnrich(this WebApplication app)
    {
        app.UseMiddleware<SerilogEnrichLogContextMiddleware>();
        return app;
    }
}