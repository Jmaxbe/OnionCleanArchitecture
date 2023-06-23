using Application;
using Infrastructure;
using StaffTimeTableAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddStaffTimeTableServices();

var app = builder.Build();

await app.ConfigureMiddleware();
app.RegisterEndpoints();

app.Run();