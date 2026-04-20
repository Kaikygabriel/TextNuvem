using Microsoft.EntityFrameworkCore;
using TextNuvem.Api.Extensions;
using TextNuvem.Application.Ioc;
using TextNuvem.Infra.Data.Context;
using TextNuvem.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("DefaultConnection") ??
                 throw new Exception("ConnectionString not found !");

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfra(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(x =>
    x.UseSqlServer(
        connection,x => x.MigrationsAssembly(typeof(Program).Assembly)));

builder.Services.AddDocumentation();
builder.Services.AddCorsFromApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseExceptionGlobalHandler();

app.MapOpenApi();

app.UseSwaggerUI(x=>x.SwaggerEndpoint("/openapi/v1.json","v1"));

app.UseHttpsRedirection();

app.UseCors("AllowBlazorWasm");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();