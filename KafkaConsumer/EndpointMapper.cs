using KafkaFlow;

namespace KafkaConsumer;

public static class EndpointMapper
{
    public static WebApplication RegisterEndpoints(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        
        return app;
    }
    
    public static async Task<WebApplication> StartKafkaBus(this WebApplication app)
    {
        var kafkaBus = app.Services.CreateKafkaBus();
        await kafkaBus.StartAsync();

        return app;
    }
}