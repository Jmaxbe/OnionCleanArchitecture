using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Application.Common.Models;

public class AccountResponseDto
{
    public string UserName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public List<string> Roles { get; set; }
}