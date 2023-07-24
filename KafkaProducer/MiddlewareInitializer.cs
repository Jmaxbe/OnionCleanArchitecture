using KafkaProducer.Common.Extensions;
using KafkaProducer.Middleware.ErrorHandling;

namespace KafkaProducer;

public static class MiddlewareInitializer
{
    public static async Task<WebApplication> ConfigureMiddleware(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsLocal())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<GlobalErrorHandlingMiddleware>();
        
        return app;
    }
}