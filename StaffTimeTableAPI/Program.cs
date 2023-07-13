using System.Reflection;
using Application;
using IdentityServices;
using Infrastructure;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using StaffTimeTableAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddSerilog();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    //TODO:ADD correlation id and other enrich
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(builder.Configuration["ElasticConfiguration:Uri"]!))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{"Environment"?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    })
    .CreateLogger();

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddStaffTimeTableServices(builder.Configuration);

var app = builder.Build();

await app.ConfigureMiddleware();
app.RegisterEndpoints();

app.Run();