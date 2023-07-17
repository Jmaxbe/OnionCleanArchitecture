using System.Net.Http.Headers;
using Application.Common.Interfaces;
using IdentityServices.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServices;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IKeyCloakApi, KeyCloakApiService>(client =>
        {
            client.BaseAddress = new Uri(configuration["Keycloak:BaseAddress"]!);
        });
        
        return services;
    }
}