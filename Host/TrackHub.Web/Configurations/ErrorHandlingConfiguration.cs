using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TrackHub.Web.Configurations;

public static class ErrorHandlingConfiguration
{
    public static void AddErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerFeature>();
                var ex = feature?.Error;

                var traceId = context.TraceIdentifier;

                app.Logger.LogError(ex, "Unhandled exception. TraceId={TraceId}", traceId);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/problem+json";

                var pd = new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Status = 500,
                    Detail = "An unexpected error occurred. Provide the traceId to support.",
                    Instance = context.Request.Path
                };

                pd.Extensions["traceId"] = traceId;

                await context.Response.WriteAsJsonAsync(pd);
            });
        });
    }
}
