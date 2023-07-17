namespace Application.Common.Models.Dto.Keycloak.Request;

public class Credentials
{
    public string Type { get; set; }
    public string Value { get; set; }
    public bool Temporary { get; set; } = true;
}