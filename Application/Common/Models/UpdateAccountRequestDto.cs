namespace Application.Common.Models;

public class UpdateAccountRequestDto
{
    public string InitialUserName { get; set; }
    public string UserName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}