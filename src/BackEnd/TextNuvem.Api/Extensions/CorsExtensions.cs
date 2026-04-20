namespace TextNuvem.Api.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsFromApplication(this IServiceCollection service)
    {
        service.AddCors(options =>
        {
            options.AddPolicy("AllowBlazorWasm", policy =>
            {
                policy.WithOrigins("http://localhost:5255") 
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        return service;
    }
}