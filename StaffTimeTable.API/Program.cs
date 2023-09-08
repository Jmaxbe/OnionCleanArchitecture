using IdentityServices;
using Microsoft.IdentityModel.Logging;
using Prometheus;
using Serilog;
using StaffTimeTable.API;
using StaffTimeTable.API.Services;
using StaffTimetable.Application;
using StaffTimetable.Application.Common.Interfaces;
using StaffTimetable.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
    IdentityModelEventSource.ShowPII = true;
}

builder.Services.AddSwagger();

builder.Services.AddJwt(builder.Configuration);

builder.Services.AddAuthorization();

//builder.Services.AddLogger(builder.Configuration);

builder.Services.AddJaeger();

var app = builder.Build();

await app.ConfigureMiddleware();

app.UseMetricServer();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSerilogRequestLogging();

app.UseRouting();
        
app.UseHttpMetrics(options=>
{
    options.AddCustomLabel("host", context => context.Request.Host.Host);
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

namespace StaffTimeTable.API
{
    public partial class Program { }
}