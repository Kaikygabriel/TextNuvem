using Microsoft.EntityFrameworkCore;
using Npgsql;
using TextNuvem.Api.Extensions;
using TextNuvem.Application.Ioc;
using TextNuvem.Infra.Data.Context;
using TextNuvem.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("DefaultConnection") ??
                 throw new Exception("ConnectionString not found !");

var dataSourceBuilder =
    new NpgsqlDataSourceBuilder(connection);
dataSourceBuilder.EnableDynamicJson();
var dataSource = dataSourceBuilder.Build();


builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfra(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(dataSource,b => b.MigrationsAssembly("TextNuvem.Api"));
});

builder.Services.AddDocumentation();
builder.Services.AddCorsFromApplication();

var app = builder.Build();

var port = Environment.GetEnvironmentVariable("PORT");

if (!string.IsNullOrWhiteSpace(port))
    app.Urls.Add($"http://*:{port}");

if (app.Environment.IsDevelopment())
    app.UseExceptionGlobalHandler();

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseSwaggerUI(x=>x.SwaggerEndpoint("/openapi/v1.json","v1"));

app.UseCors("AllowBlazorWasm");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();