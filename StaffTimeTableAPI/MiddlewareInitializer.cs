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
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }

        app.UseMiddleware<GlobalErrorHandlingMiddleware>();
        
        return app;
    }
}