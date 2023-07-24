using KafkaConsumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKafkaConsumerServices(builder.Configuration);

var app = builder.Build();

await app.ConfigureMiddleware();
app.RegisterEndpoints();

app.Run();