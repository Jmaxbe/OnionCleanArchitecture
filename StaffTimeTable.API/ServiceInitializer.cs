using System.Security.Claims;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders;
using Jaeger.Senders.Thrift;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using OpenTracing;
using OpenTracing.Util;
using Serilog;
using StaffTimeTable.API.Filters;
using StaffTimeTable.API.Services;
using StaffTimetable.Application.Common.Interfaces;

namespace StaffTimeTable.API;

public static class ServiceInitializer
{
    /// <summary>
    /// Adds the Swagger Documentation
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Staff Time-Table",
                Description = "Demo API - Clean Architecture Solution in .NET 7",
            });
            options.AddSecurityDefinition("Token", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Description = "Please enter a valid token",
                Name = HeaderNames.Authorization,
                In = ParameterLocation.Header,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });
            options.OperationFilter<SecureEndpointAuthRequirementFilter>();
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "StaffTimeTable.API.xml"));
        });
    }

    /// <summary>
    /// Adds the JWT token
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.
                AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.
                AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.Authority = configuration["Jwt:Authority"];
            o.Audience = configuration["Jwt:Audience"];
            o.RequireHttpsMetadata = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                RequireAudience = true,
                RequireExpirationTime = true,
                RoleClaimType = ClaimTypes.
                    Role,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidAudience = configuration["Jwt:Audience"],
                ValidIssuer = configuration["Jwt:Authority"],
                ValidateIssuerSigningKey = true
            };
        });
    }

    /// <summary>
    /// Adds the Serilog to logging
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddLogger(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        services.AddLogging(builder =>
        {
            builder.AddSerilog();
        });
    }

    /// <summary>
    /// Adds the Jaeger Tracer
    /// </summary>
    /// <param name="services"></param>
    public static void AddJaeger(this IServiceCollection services)
    {
        services.AddOpenTracing();
        
        services.AddSingleton<ITracer>(sp =>
        {
            var serviceName = sp.GetRequiredService<IWebHostEnvironment>().ApplicationName;
            var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
            var reporter = new RemoteReporter.Builder().WithLoggerFactory(loggerFactory).WithSender(new UdpSender())
                .Build();
            var tracer = new Tracer.Builder(serviceName)
                // The constant sampler reports every span.
                .WithSampler(new ConstSampler(true))
                // LoggingReporter prints every reported span to the logging framework.
                .WithReporter(reporter)
                .Build();
            return tracer;
        });
    }
}