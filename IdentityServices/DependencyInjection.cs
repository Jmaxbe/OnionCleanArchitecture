﻿using System.Net.Http.Headers;
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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJVVGI3eUhkTU5TMjBacVRyWHFieW9ObEhPUkM1TTlONWZ4RF9EVnItejhVIn0.eyJleHAiOjE2ODkwMTQ3MjksImlhdCI6MTY4ODk5NjcyOSwianRpIjoiMWJhNjczNDUtOTQ1ZS00YWZiLTgxODUtMmFkMzU5YWM3Y2Q3IiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo4MDgwL3JlYWxtcy9zdGFmZi10aW1ldGFibGUtcmVhbG0iLCJhdWQiOlsic3RhZmYtdGltZXRhYmxlLWNsaWVudCIsImFjY291bnQiXSwic3ViIjoiZjUwNjM3NmMtZGY3OC00ZTZhLThjMTktMzdhZDQzNGQwOTk1IiwidHlwIjoiQmVhcmVyIiwiYXpwIjoic3RhZmYtdGltZXRhYmxlLWNsaWVudCIsInNlc3Npb25fc3RhdGUiOiI1NDY5N2YxMy04NmYxLTRiOWQtYTRkMi01NzM0NWRlMjViMmYiLCJhY3IiOiIxIiwiYWxsb3dlZC1vcmlnaW5zIjpbIi8qIl0sInJlc291cmNlX2FjY2VzcyI6eyJhY2NvdW50Ijp7InJvbGVzIjpbIm1hbmFnZS1hY2NvdW50IiwibWFuYWdlLWFjY291bnQtbGlua3MiLCJ2aWV3LXByb2ZpbGUiXX19LCJzY29wZSI6InByb2ZpbGUgZ29vZC1zZXJ2aWNlIGVtYWlsIiwic2lkIjoiNTQ2OTdmMTMtODZmMS00YjlkLWE0ZDItNTczNDVkZTI1YjJmIiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJvZmZsaW5lX2FjY2VzcyIsInVtYV9hdXRob3JpemF0aW9uIiwiZGVmYXVsdC1yb2xlcy1zdGFmZi10aW1ldGFibGUtcmVhbG0iLCJBZG1pbiIsIk1lbWJlciJdLCJuYW1lIjoiQWRtaW4gQWRtaW4iLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiIsImdpdmVuX25hbWUiOiJBZG1pbiIsImZhbWlseV9uYW1lIjoiQWRtaW4ifQ.Dj-mAp3mDZc-qcqwPrY7kDdLrBZ5AgPOklCeT17M9cF8vnldks2qE8uiYA5D-dxkf_6Ma6AKAkfrKc7AToxF5gjrd9psxqdR61NJCYqruNgDfO1qF2p8n1_gahmOWVPQA34UtiXdPO9HZY4FRHEO-ci_lbNkj9UAh3U0U17wBXQrIQoveZHl29WG5XdtgLMvl6j77F4GXnceTvd4oLWJG8tpj24s_lUKGHkdR0OqNBWp_aCiqtrD015guTCKClwp172gz2Edcz-A6aTVbFryUBSKdpgGxfr0eYNnVWJ2AgCgnNSyOj4otdIQBKcjSuAZcR6iynKZcQ7JD_TO18VdfA");
        });
        
        return services;
    }
}