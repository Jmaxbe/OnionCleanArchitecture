using Microsoft.EntityFrameworkCore;
using Serilog;

namespace StaffTimetable.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitializer(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            Log.Error("{@name} => {@ex}", nameof(ApplicationDbContextInitializer), ex);
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
            Log.Error("{@name} => {@ex}", nameof(ApplicationDbContextInitializer), ex);
            throw;
        }
    }

    private async Task TrySeedAsync()
    {

        await _context.SaveChangesAsync();
    }
}