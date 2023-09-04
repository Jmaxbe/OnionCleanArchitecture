using IdentityServices;
using StaffTimeTable.API;
using StaffTimetable.Application;
using StaffTimetable.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddStaffTimeTableServices(builder.Configuration);

var app = builder.Build();

await app.ConfigureMiddleware();
app.RegisterEndpoints();

app.Run();

namespace StaffTimeTable.API
{
    public partial class Program { }
}