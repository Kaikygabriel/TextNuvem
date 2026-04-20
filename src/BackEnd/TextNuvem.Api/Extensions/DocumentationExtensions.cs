using Microsoft.OpenApi;

namespace TextNuvem.Api.Extensions;

internal static class DocumentationExtensions
{
    public static IServiceCollection AddDocumentation(this IServiceCollection services)
    {
        services.AddOpenApi("v1",x =>
            x.AddDocumentTransformer((document, context, cl) =>
            {
                document.Servers =
                [
                    new OpenApiServer(){Url = "https://localhost:7249",Description = "Server in https"},
                    new OpenApiServer(){Url = "http://localhost:5167",Description = "Server in http"}
                ];
                document.Info = new OpenApiInfo()
                {
                    Title = "TextNuvem ",
                    Version = "V1"
                };
                return Task.CompletedTask;
            }));
        return services;
    }
}