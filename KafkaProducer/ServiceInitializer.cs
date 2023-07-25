using System.Security.Claims;
using Confluent.Kafka;
using KafkaFlow;
using KafkaFlow.Serializer;
using KafkaProducer.Common;
using KafkaProducer.Filters;
using KafkaProducer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using Acks = KafkaFlow.Acks;

namespace KafkaProducer;

public static class ServiceInitializer
{
    public static IServiceCollection AddKafkaProducerServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IProducerService, ProducerService>();
        
        services.AddHttpContextAccessor();

        services.AddControllers();

        AddSwagger(services);

        AddJwt(services, configuration);
        
        services.AddAuthorization();
        
        AddLogging(services, configuration);
        
        AddBroker(services, configuration);

        return services;
    }

    private static void AddSwagger(IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Kafka Producer API",
                Description = "Demo API - Clean Architecture Solution in .NET 7",
            });
            options.AddSecurityDefinition("Token", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Description = "Please enter a valid token",
                Name = HeaderNames.Authorization,
                In = ParameterLocation.Header,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });
            options.OperationFilter<SecureEndpointAuthRequirementFilter>();
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "KafkaProducer.xml"));
        });
    }

    private static void AddJwt(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.
                AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.
                AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.Authority = configuration["Jwt:Authority"];
            o.Audience = configuration["Jwt:Audience"];
            o.RequireHttpsMetadata = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                RequireAudience = true,
                RequireExpirationTime = true,
                RoleClaimType = ClaimTypes.
                    Role,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidAudience = configuration["Jwt:Audience"],
                ValidIssuer = configuration["Jwt:Authority"],
                ValidateIssuerSigningKey = true
            };
        });
    }

    private static void AddLogging(IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        services.AddLogging(builder =>
        {
            builder.AddSerilog();
        });
    }
    
    private static void AddBroker(IServiceCollection services, IConfiguration configuration)
    {
        var topic = "kafka-topic-sample";
        
        services.AddKafka(kafka => kafka
            .UseMicrosoftLog()
            .AddCluster(cluster => cluster
                .WithBrokers(configuration.GetSection("KafkaHosts").Get<string[]>())
                .CreateTopicIfNotExists(topic, 8, 1)
                .AddProducer(KafkaConstants.ProducerName, producer => producer
                    .DefaultTopic(topic)
                    // .WithAcks(Acks.All)
                    // .WithCompression(CompressionType.Gzip)
                    .AddMiddlewares(m=>m.AddSerializer<JsonCoreSerializer>())
                )
            )
        );
    }
    
    //TODO:SAGA
    //TODO:Retry Polly
    //TODO:https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-circuit-breaker-pattern
    //TODO:circuit breaker c#
    //TODO:Интеграционные тесты

    //TODO:Health chech
    //TODO:Add gateway
    //TODO:api gateway c# ocelot
    //TODO:Yarp.ReverseProxy и описать выбор
    //TODO:Начать по ключевым точкам системы составлять список
    //TODO:3 репозитория главный, HTTP, общается с Kafka
}