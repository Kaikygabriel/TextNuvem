using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TextNuvem.Application.Query;
using TextNuvem.Application.Services;
using TextNuvem.Domain.BackOffice.Repositories;
using TextNuvem.Infra.Configure;
using TextNuvem.Infra.Query;
using TextNuvem.Infra.Repositories;
using TextNuvem.Infra.Services;

namespace TextNuvem.Infra.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddOptions<ConfigurationJwt>()
            .BindConfiguration("Jwt")
            .ValidateOnStart()
            .ValidateDataAnnotations();
        
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddTransient<ICustomerQuery,CustomerQuery>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddTransient<IProjectQuery,ProjectQuery>();
        
        services.AddTransient<ITokenService,TokenService>();
        services.AddScoped<ICompactorService,CompactorService>();
        
        services.AddTransient<IUnitOfWork,UnitOfWork>();

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });
        services.AddAuthorization();
        
        return services;
    }
}