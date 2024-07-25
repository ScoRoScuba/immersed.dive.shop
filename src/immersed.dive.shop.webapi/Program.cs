using System;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Serilog;
using immersed.dive.shop.webapi.Extensions.Startup;
using immersed.dive.shop.webapi.Infrastructure.Handlers;

var builder = WebApplication.CreateBuilder(args);

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, false)
    .AddJsonFile($"appsettings.{env.ToString().ToLower()}.json", true)
    .AddUserSecrets<Program>(true)
    .AddEnvironmentVariables()
    .Build();
    
builder.Configuration.AddConfiguration(configuration);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddVersioning(configuration);

builder.Services.AddTelemetry(configuration);

builder.Services.AddServices(configuration);

builder.Services.AddDataStore(configuration);

builder.Services.AddThirdPartyServices(configuration);

//builder.Services.AddDapper(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenDoc(configuration);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy => policy
        .WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowCredentials()
        .WithExposedHeaders("WWW-Authenticate")
        .AllowAnyHeader());
});

builder.Services.AddHealthChecks();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseOpenDoc(configuration);
}

// Configure the HTTP request pipeline.

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

await DataStore.RunMigrations( app.Services, args);

app.Run();

