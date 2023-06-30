namespace Application.Common.Models;

public class CreateAccountRequestDto
{
    public string UserName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Password { get; set; }
}