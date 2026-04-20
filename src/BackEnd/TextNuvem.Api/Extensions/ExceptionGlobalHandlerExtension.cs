using Microsoft.AspNetCore.Diagnostics;

namespace TextNuvem.Api.Extensions;

internal static class ExceptionGlobalHandlerExtension
{
    public static WebApplication UseExceptionGlobalHandler(this WebApplication app)
    {
        app.UseExceptionHandler(x =>
            x.Run(async (context) =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var error = context.Features.Get<IExceptionHandlerFeature>();
                if (error is not null)
                    await context.Response.WriteAsJsonAsync(new
                    {
                        error.Error.Message,
                        error.Error.StackTrace,
                        error.Error.Source,
                        error.RouteValues
                    });
            }));
        
        return app;
    }
}