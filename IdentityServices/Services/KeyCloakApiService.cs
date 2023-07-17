using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Models.Dto.Keycloak.Request;
using IdentityServices.Models;
using Microsoft.Extensions.Configuration;

namespace IdentityServices.Services;

public class KeyCloakApiService : IKeyCloakApi
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly JsonSerializerOptions _camelCaseSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    private string ClientId => _configuration["Keycloak:ClientId"]!;
    private string Realm => _configuration["Keycloak:Realm"]!;
    private string ClientSecret => _configuration["Keycloak:ClientSecret"]!;

    public KeyCloakApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    
    public async Task<Guid> CreateUser(CreateUserDto user)
    {
        await SetClientTokenAuth();
        var response = await PostWithStringContentAsync($"admin/realms/{Realm}/users",
            JsonSerializer.Serialize(user, _camelCaseSerializerOptions));

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Cannot create keycloak user. Reason: {response.ReasonPhrase}");

        var createdUser = await GetUserByUserName(user.Username);

        var assignRoles = await AssignRoles(createdUser.Id, user.UserRoles);

        if (assignRoles.Succeeded) return createdUser.Id;
        
        await DeleteUserByIdAsync(createdUser.Id);
        
        throw new Exception($"Cannot create user. Reasons: [{string.Join("; ", assignRoles.Errors)}]");
    }

    private async Task<HttpResponseMessage> PostWithStringContentAsync(string requestUri, string json,
        string mediaType = "application/json", CancellationToken cancellationToken = default)
    {
        var payload = new StringContent(json, Encoding.Default, mediaType);
        var response = await _httpClient.PostAsync(requestUri, payload, cancellationToken);

        return response;
    }

    private async Task SetClientTokenAuth()
    {
        var clientToken = await GetClientToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", clientToken.AccessToken);
    }

    private async Task<JwtResponse> GetClientToken(CancellationToken cancellationToken = default)
    {
        var response = await PostWithKeyValuePairAsync(
            $"realms/{Realm}/protocol/openid-connect/token",
            GetParamsOfClientToken(), cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Cannot get keycloak client token. Reason: {response.ReasonPhrase}");

        return JsonSerializer.Deserialize<JwtResponse>(await response.Content.ReadAsStringAsync(cancellationToken))!;
    }
    
    private async Task<HttpResponseMessage> PostWithKeyValuePairAsync(string requestUri, IEnumerable<KeyValuePair<string, string>> valuePairs, 
        CancellationToken cancellationToken = default)
    {
        var payload = new FormUrlEncodedContent(valuePairs);
        var response = await _httpClient.PostAsync(requestUri, payload, cancellationToken);

        return response;
    }

    private List<KeyValuePair<string, string>> GetParamsOfClientToken()
    {
        return new List<KeyValuePair<string, string>>()
        {
            new("grant_type", GrantTypeConstant.ClientCredentials),
            new("client_id", ClientId),
            new("client_secret", ClientSecret)
        };
    }

    private async Task<Result> AssignRoles(Guid userId, IEnumerable<string> roles, CancellationToken cancellationToken = default)
    {
        var realmRoles = await GetRealmRolesByNames(roles, cancellationToken);

        if (!realmRoles.result.Succeeded)
            return realmRoles.result;

        var response = await PostWithStringContentAsync($"admin/realms/{Realm}/users/{userId}/role-mappings/realm",
            JsonSerializer.Serialize(realmRoles.roles, _camelCaseSerializerOptions),
            cancellationToken: cancellationToken);

        return !response.IsSuccessStatusCode ? Result.Failure(new []{$"Cannot assign the roles, reason phrase: {response.ReasonPhrase}, Status code {response.StatusCode}"}) : Result.Success();
    }

    private async Task<ClientResponse> GetClientInfo(CancellationToken cancellationToken = default)
    {
        var response =
            await _httpClient.GetAsync($"admin/realms/{Realm}/clients/?clientId={ClientId}", cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new Exception(
                $"Cannot obtain client {ClientId} information, reason phrase: {response.ReasonPhrase}, Status code {response.StatusCode}");
        
        return JsonSerializer.Deserialize<List<ClientResponse>>(await response.Content.ReadAsStringAsync(cancellationToken), _camelCaseSerializerOptions)!.First();
    }

    private async Task<UserResponse> GetUserByUserName(string userName, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"admin/realms/{Realm}/users?username={userName}", cancellationToken);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception(
                $"Cannot find user with user name={userName}, reason phrase: {response.ReasonPhrase}");
        
        return JsonSerializer
            .Deserialize<List<UserResponse>>(await response.Content.ReadAsStringAsync(cancellationToken),
                _camelCaseSerializerOptions)!.First();
    }

    private async Task<UserResponse> GetUserById(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"admin/realms/{Realm}/users/{id}", cancellationToken);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception(
                $"Cannot find user with id={id}, reason phrase: {response.ReasonPhrase}");

        return JsonSerializer
            .Deserialize<List<UserResponse>>(await response.Content.ReadAsStringAsync(cancellationToken),
                _camelCaseSerializerOptions)!.First();
    }

    private async Task<RoleResponse> GetClientRoleByName(string roleName, CancellationToken cancellationToken = default)
    {
        var clientInfo = await GetClientInfo(cancellationToken);
        var response = await _httpClient.GetAsync($"admin/realms/{Realm}/clients/{clientInfo.Id}/roles/{roleName}", cancellationToken);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception(
                $"Cannot find role with name={roleName}, reason phrase: {response.ReasonPhrase}");
        
        return JsonSerializer
            .Deserialize<RoleResponse>(await response.Content.ReadAsStringAsync(cancellationToken),
                _camelCaseSerializerOptions)!;
    }
    
    private async Task<(List<RoleResponse> roles, Result result)> GetRealmRolesByNames(IEnumerable<string> roleNames, CancellationToken cancellationToken = default)
    {
        var responseList = new ConcurrentBag<RoleResponse>();
        var errors = new List<string>();

        await Parallel.ForEachAsync(roleNames, cancellationToken, async (roleName, ct) =>
        {
            var response = await _httpClient.GetAsync($"admin/realms/{Realm}/roles/{roleName}", ct);
            
            if (!response.IsSuccessStatusCode)
                errors.Add($"Cannot find role with name={roleName}, reason phrase: {response.ReasonPhrase}");

            var deserialized = JsonSerializer
                .Deserialize<RoleResponse>(await response.Content.ReadAsStringAsync(cancellationToken),
                    _camelCaseSerializerOptions);
            
            responseList.Add(deserialized!);
        });

        return (responseList.ToList(), errors.Any() ? Result.Failure(errors) : Result.Success());
    }

    private async Task DeleteUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync($"admin/realms/{Realm}/users/{userId}", cancellationToken);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception(
                $"Cannot delete user with id={userId}, reason: {response.ReasonPhrase}");
    }
}