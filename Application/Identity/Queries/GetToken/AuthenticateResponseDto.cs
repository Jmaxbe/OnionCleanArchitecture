namespace Application.Identity.Queries.GetToken;

public class AuthenticateResponseDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}