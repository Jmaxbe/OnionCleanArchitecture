using Application;
using IdentityServices;
using Infrastructure;
using ToDoAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddToDoServices(builder.Configuration);

var app = builder.Build();

await app.ConfigureMiddleware();
app.RegisterEndpoints();

app.Run();
