using Application.Common.Interfaces;
using StaffTimeTableAPI.Services;

namespace StaffTimeTableAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddStaffTimeTable(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddControllersWithViews();

        return services;
    }
}