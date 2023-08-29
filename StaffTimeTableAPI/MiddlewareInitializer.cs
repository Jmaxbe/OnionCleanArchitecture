using Infrastructure.Persistence;
using StaffTimeTableAPI.Middleware.ErrorHandling;

namespace StaffTimeTableAPI;

public static class MiddlewareInitializer
{
    public static async Task<WebApplication> ConfigureMiddleware(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            using var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
            await initializer.InitialiseAsync();
            await initializer.SeedAsync();
        }

        app.UseMiddleware<GlobalErrorHandlingMiddleware>();
        
        return app;
    }
}