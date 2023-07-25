using Confluent.Kafka;
using KafkaFlow;
using KafkaFlow.Producers;

namespace MessageBroker.Kafka.Services;

public class SomeProducer : ISomeProducer
{
    private readonly IMessageProducer _messageProducer;

    public SomeProducer(IProducerAccessor producerAccessor)
    {
        _messageProducer = producerAccessor.GetProducer("") ?? throw new ArgumentException();
    }

    public async Task PublishAsync(string trigger, string? email, byte[] @event,
        CancellationToken cancellationToken)
    {
        const string defaultMessageKey = "unknown";

        await _messageProducer.ProduceAsync((object)(email ?? defaultMessageKey), @event,
            new MessageHeaders(new Headers()
            {
                {
                    "trigger", System.Text.Encoding.UTF8.GetBytes(trigger)
                }
            }));
    }
}