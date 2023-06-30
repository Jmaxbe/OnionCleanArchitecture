using Application.Common.Interfaces;
using Application.Common.Models;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IIdentityService _identityService;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, IIdentityService identityService)
    {
        _logger = logger;
        _context = context;
        _identityService = identityService;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
    
    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        if (!_context.Roles.Any())
        {
            _context.Roles.Add(new IdentityRole<int>
            {
                Name = RoleConstants.Administrator,
                NormalizedName = RoleConstants.Administrator.Normalize()
            });
            _context.Roles.Add(new IdentityRole<int>
            {
                Name = RoleConstants.Moderator,
                NormalizedName = RoleConstants.Moderator.Normalize()
            });
            _context.Roles.Add(new IdentityRole<int>
            {
                Name = RoleConstants.Member,
                NormalizedName = RoleConstants.Member.Normalize()
            });
        }

        if (!_context.Users.Any())
        {
            await _identityService.CreateUserAsync(new CreateAccountRequestDto
            {
                UserName = "Administrator",
                Email = "administrator@mail.ru",
                Phone = null,
                Password = "Qwerty123!"
            });
        }

        await _context.SaveChangesAsync();
    }
}