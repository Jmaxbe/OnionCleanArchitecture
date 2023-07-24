using System.Security.Claims;
using System.Text.Json;
using Confluent.Kafka;
using KafkaConsumer.Common;
using KafkaConsumer.Filters;
using KafkaConsumer.Handlers;
using KafkaConsumer.Middleware;
using KafkaConsumer.Services;
using KafkaFlow;
using KafkaFlow.Serializer;
using KafkaFlow.TypedHandler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using AutoOffsetReset = KafkaFlow.AutoOffsetReset;

namespace KafkaConsumer;

public static class ServiceInitializer
{
    public static IServiceCollection AddKafkaConsumerServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IConsumerService, ConsumerService>();
        
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
                Title = "Kafka Consumer API",
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
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "KafkaConsumer.xml"));
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
                .AddConsumer(consumer => consumer
                    .Topic(topic)
                    //.WithName($"print-console-name{1}")
                    .WithGroupId("kafka-topic-sample-print-console-handler")
                    .WithBufferSize(100)
                    .WithWorkersCount(3)
                    //.WithAutoOffsetReset(AutoOffsetReset.Earliest)
                    .AddMiddlewares(middlewares => middlewares
                        // .AddSerializer(resolver => new NewtonsoftJsonSerializer(new JsonSerializerSettings
                        // {
                        //     ContractResolver = new DefaultContractResolver
                        //     {
                        //         NamingStrategy = new SnakeCaseNamingStrategy()
                        //     }
                        // }))
                        .AddSerializer<JsonCoreSerializer>()
                        //.Add<ProcessMessagesMiddleware>()
                        .AddTypedHandlers(h=>h.AddHandler<PrintConsoleHandler>())
                    )
                )
            )
        );
    }
}