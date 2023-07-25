using KafkaProducer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKafkaProducerServices(builder.Configuration);

var app = builder.Build();

await app.ConfigureMiddleware();

app.RegisterEndpoints();

await app.StartKafkaBus();

await app.RunAsync();