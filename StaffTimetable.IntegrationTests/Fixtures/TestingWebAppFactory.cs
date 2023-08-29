using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace StaffTimetable.IntegrationTests.Fixtures;

public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql("User ID =postgres;Password=postgres;Server=host.docker.internal;Port=4324;Database=postgres;Integrated Security=true;Pooling=true;");
            });
            
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            
            using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            try
            {
                appContext.Database.EnsureCreated();
                appContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                //Log errors or do anything you think it's needed
                throw;
            }
        });
    }
}