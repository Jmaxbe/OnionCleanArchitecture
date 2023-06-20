using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        IConfigurationBuilder builder = new ConfigurationBuilder();
        IConfigurationRoot config = builder.Build();
        string? connectionString = config.GetConnectionString("DefaultConnection");
        
        optionsBuilder.UseSqlServer(connectionString);
    
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}