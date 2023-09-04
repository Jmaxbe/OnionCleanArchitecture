using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StaffTimetable.Application.Common.Interfaces;
using StaffTimetable.Persistence.Interceptors;
using StaffTimetable.Persistence.Repository;
using StaffTimetable.Persistence.Services;

namespace StaffTimetable.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("ApplicationConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });
        
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddTransient<IDateTime, DateTimeService>();

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }
}