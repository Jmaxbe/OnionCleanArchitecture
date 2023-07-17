using System.Text.Json.Serialization;

namespace Application.Common.Models.Dto.Keycloak.Request;

public class CreateUserDto
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public bool Enabled { get; set; } = true;
    public Dictionary<string, string> Attributes { get; set; }
    public List<string> Groups { get; set; }
    [JsonIgnore]
    public List<string> UserRoles { get; set; }
    public List<Credentials> Credentials { get; set; }
}