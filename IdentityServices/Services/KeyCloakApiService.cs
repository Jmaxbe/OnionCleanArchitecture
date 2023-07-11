using System.Text;
using System.Text.Json;
using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace IdentityServices.Services;

public class KeyCloakApiService : IKeyCloakApi
{
    private readonly HttpClient _httpClient;

    public KeyCloakApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<bool> CreateUser()
    {
        var schema = new
        {
            Credentials = new
            {
                CredentialData = "test",
                SecretData = "test",
                Temporary = false,
                UserLabel = "test",
                Value = "test"
            }
        };
        var payload = new StringContent(JsonSerializer.Serialize(schema), Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync("staff-timetable-realm/users", payload);//TODO:Doesn't work with documentation

        return result.IsSuccessStatusCode;
    }
}