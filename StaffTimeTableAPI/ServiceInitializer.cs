using System.Security.Claims;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using StaffTimeTableAPI.Filters;
using StaffTimeTableAPI.Services;

namespace StaffTimeTableAPI;

public static class ServiceInitializer
{
    public static IServiceCollection AddStaffTimeTableServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddControllers();

        AddSwagger(services);
        
        IdentityModelEventSource.ShowPII = true;
        
        AddJwt(services, configuration);
        
        services.AddAuthorization();
        
        AddLogging(services, configuration);

        return services;
    }

    private static void AddSwagger(IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "StaffTimeTableAPI.xml"));
        });
    }

    private static void AddJwt(IServiceCollection services, IConfiguration configuration)
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

    private static void AddLogging(IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        services.AddLogging(builder =>
        {
            builder.AddSerilog();
        });
    }
}