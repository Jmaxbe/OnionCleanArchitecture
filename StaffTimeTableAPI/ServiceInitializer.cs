using Application.Common.Interfaces;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using StaffTimeTableAPI.Filters;
using StaffTimeTableAPI.Services;

namespace StaffTimeTableAPI;

public static class ServiceInitializer
{
    public static IServiceCollection AddStaffTimeTableServices(this IServiceCollection services)
    {
        AddCustomDependencies(services);
        
        services.AddHttpContextAccessor();

        services.AddControllers();

        AddSwagger(services);

        return services;
    }

    private static void AddCustomDependencies(IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
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
}