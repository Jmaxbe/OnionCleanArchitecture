namespace StaffTimeTableAPI;

public static class EndpointMapper
{
    public static WebApplication RegisterEndpoints(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        
        app.UseAuthorization();
        
        app.MapControllers();
        
        return app;
    }
}