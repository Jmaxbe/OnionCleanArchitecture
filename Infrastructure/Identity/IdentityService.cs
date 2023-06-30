using System.IdentityModel.Tokens.Jwt;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Identity.Queries.GetToken;
using Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public IdentityService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    
    public async Task<string?> GetUserNameByIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId, cancellationToken);

        return user.UserName;
    }

    public async Task<AuthenticateResponseDto> AuthenticateByEmail(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(_userManager.NormalizeEmail(email));

        if (user is null)
        {
            throw new NotFoundException($"User with email {email} does not exists");
        }

        return await Authenticate(user, password);
    }
    
    public async Task<AuthenticateResponseDto> AuthenticateByUserName(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(_userManager.NormalizeName(userName));
        if (user is null) throw new NotFoundException($"User with name {userName} does not exists");

        return await Authenticate(user, password);
    }

    private async Task<AuthenticateResponseDto> Authenticate(ApplicationUser user, string password)
    {
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
        {
            throw new BadCredentials("Bad credentials");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var refreshToken = _configuration.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(_configuration.GetSection("TokenValidityInMinutes").Get<int>());
        await _userManager.UpdateAsync(user);

        return new AuthenticateResponseDto
        {
            Token = GenerateToken(user, roles),
            RefreshToken = refreshToken
        };
    }

    public async Task<AccountResponseDto> CreateUserAsync(CreateAccountRequestDto data)
    {
        var user = new ApplicationUser
        {
            UserName = data.UserName,
            NormalizedUserName = _userManager.NormalizeName(data.UserName),
            Email = data.Email,
            NormalizedEmail = _userManager.NormalizeEmail(data.Email),
            PhoneNumber = data.Phone,
            PhoneNumberConfirmed = false,
            RefreshToken = null,
            RefreshTokenExpiryTime = default
        };

        if (_userManager.Users.Any(_ => _.NormalizedEmail == _userManager.NormalizeEmail(data.Email)))
            throw new AlreadyExists($"User with email {data.Email} already exists");

        if (_userManager.Users.Any(_ => _.NormalizedUserName == _userManager.NormalizeName(data.UserName)))
            throw new AlreadyExists($"User with login {data.UserName} already exists");

        var addUserResult = await _userManager.CreateAsync(user, data.Password);
        if (!addUserResult.Succeeded) throw new ValidationException(addUserResult.ToApplicationResult().Errors);

        var addRolesResult = await _userManager.AddToRolesAsync(user, new List<string> { RoleConstants.Member });
        if (!addRolesResult.Succeeded) throw new ValidationException(addUserResult.ToApplicationResult().Errors);
        var roles = await _userManager.GetRolesAsync(user);

        return new AccountResponseDto
        {
            UserName = user.UserName,
            Email = user.Email,
            Phone = user.PhoneNumber,
            Roles = roles.ToList()
        };
    }
    
    public async Task<AccountResponseDto> UpdateUserCredentials(UpdateAccountRequestDto data)
    {
        var user = await _userManager.FindByNameAsync(data.InitialUserName);
        if (user is null) throw new NotFoundException($"User with name {data.InitialUserName} does not exists");

        user.UserName = data.UserName;
        user.Email = data.Email;
        user.PhoneNumber = data.Phone;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded) throw new ValidationException(updateResult.ToApplicationResult().Errors);

        var roles = await _userManager.GetRolesAsync(user);

        return new AccountResponseDto
        {
            UserName = user.UserName,
            Email = user.Email,
            Phone = user.PhoneNumber,
            Roles = roles.ToList()
        };
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            throw new ValidationException(new List<string> { $"The user with id {userId} does not exists" });
        
        await DeleteUserAsync(user);
    }

    public async Task DeleteUserByName(string userName)
    {
        var user = await _userManager.FindByNameAsync(_userManager.NormalizeName(userName));
        if (user == null)
            throw new ValidationException(new List<string> { $"The user with name {userName} does not exists" });
        
        await DeleteUserAsync(user);
    }

    private async Task DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);
        
        if (!result.Succeeded)
        {
            throw new ValidationException(result.ToApplicationResult().Errors);
        }
    }

    private string GenerateToken(ApplicationUser user, IList<string> roles)
    {
        var token = user
            .CreateClaims(roles)
            .CreateJwtToken(_configuration);
        var tokenHandler = new JwtSecurityTokenHandler();
        
        return tokenHandler.WriteToken(token);
    }
}