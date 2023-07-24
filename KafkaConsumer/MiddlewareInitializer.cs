using KafkaConsumer.Common.Extensions;
using KafkaConsumer.Middleware.ErrorHandling;

namespace KafkaConsumer;

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