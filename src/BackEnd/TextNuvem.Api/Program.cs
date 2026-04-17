using Microsoft.EntityFrameworkCore;
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
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();