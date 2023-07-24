using Confluent.Kafka;
using KafkaFlow;
using KafkaFlow.Producers;
using KafkaFlow.Serializer;
using KafkaFlow.TypedHandler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBroker.Kafka;

public static class DependencyInjection
{
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        //TODO:https://www.youtube.com/watch?v=4xpjlqIlfY8&ab_channel=NDCConferences
        services.AddKafka(kafka => kafka
            .UseMicrosoftLog()
            .AddCluster(cluster => cluster
                .WithBrokers(new[] { configuration["Kafka:BrokerHost"] })
                .AddConsumer(consumer => consumer
                    .Topic("sample-topic")
                    .WithGroupId("sample-group")
                    .WithBufferSize(100)
                    .WithWorkersCount(10)
                    // .AddMiddlewares(middlewares => middlewares
                    //     .AddSerializer<JsonCoreSerializer>()
                    //     .AddTypedHandlers(handlers => handlers
                    //         .AddHandler<SampleMessageHandler>())
                    // )
                )
                .AddProducer("producer-name", producer => producer
                    .DefaultTopic("sample-topic")
                    .AddMiddlewares(middlewares => middlewares
                        .AddSerializer<JsonCoreSerializer>()
                    )
                    .WithCompression(CompressionType.Gzip)
                )
            )
        );
        
        return services;
    }
}