using Application.Common.Models;
using Application.Identity.Queries.GetToken;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<AccountResponseDto> CreateUserAsync(CreateAccountRequestDto data);
    Task<AuthenticateResponseDto> AuthenticateByEmail(string email, string password);
    Task<AuthenticateResponseDto> AuthenticateByUserName(string userName, string password);
    Task<AccountResponseDto> UpdateUserCredentials(UpdateAccountRequestDto data);
    Task DeleteUserAsync(int userId);
    Task DeleteUserByName(string userName);
}