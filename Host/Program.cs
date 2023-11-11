using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using ServiceTemplate.Configuration;
using ServiceTemplate.DataAccess.Context;
using ServiceTemplate.DataAccess.Interfaces;
using ServiceTemplate.DataAccess.Repositories;
using ServiceTemplate.DataContracts.Interfaces;
using ServiceTemplate.Middleware;
using ServiceTemplate.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(context.Configuration));

const string baseSectionConfig = "BaseConfiguration";

var swaggerConfig = builder.Configuration.GetSection(baseSectionConfig).Get<BaseConfiguration>()?.SwaggerConfig;
var baseConfiguration = builder.Configuration.GetSection(baseSectionConfig).Get<BaseConfiguration>();
if (baseConfiguration is null)
{
    throw new InvalidDataException("Base configuration was not provided");
}

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );
builder.Services.AddTransient<ProblemDetailsFactory, BaseProblemDatailFactory>();

builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();

var databaseConfig = baseConfiguration.DatabaseConfig;
builder.Services.AddDbContext<DatabaseContext>(options => DatabaseContextFactory.CreateDbContext(options, databaseConfig.FullConnectionString));
builder.Services.AddScoped<IDatabaseContextFactory>(_ => new DatabaseContextFactory(databaseConfig.FullConnectionString));

builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();

const string customPolicyName = "CustomCors";
var origins = (builder.Configuration.GetSection($"CorsConfiguration:Origins")
        .Get<string[]>() ?? Array.Empty<string>())
    .Where(s => !string.IsNullOrEmpty(s))
    .ToArray();

builder.Services.AddCors(options => options.AddPolicy(name: customPolicyName,
    corsPolicyBuilder =>
    {
        if (!origins.Any() || origins.Any(o => o.ToLower().Equals("all")))
        {
            corsPolicyBuilder.AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(_ => true)
                .AllowCredentials();
        }
        else
        {
            corsPolicyBuilder.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins(origins.ToArray());
        }
    }));

builder.Services.AddEndpointsApiExplorer();

if (swaggerConfig is { IsEnabled: true })
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = $"Swagger of Template service",
            Description = "Swagger of Template service build with .NET CORE 7.0"
        });
    
        c.UseOneOfForPolymorphism();
        c.EnableAnnotations();
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (swaggerConfig is { IsEnabled: true })
{
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health");

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.MapControllers();

app.Run();