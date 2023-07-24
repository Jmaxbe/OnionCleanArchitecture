using Confluent.Kafka;
using KafkaFlow;
using KafkaFlow.Producers;
using KafkaProducer.Common;
using KafkaProducer.Models;

namespace KafkaProducer.Services;

public class ProducerService : IProducerService
{
    private readonly IMessageProducer _messageProducer;

    public ProducerService(IProducerAccessor producerAccessor)
    {
        _messageProducer = producerAccessor.GetProducer(KafkaConstants.ProducerName) ?? throw new AggregateException();
    }

    public async Task PublishAsync(string trigger, byte[] message)
    {
         await _messageProducer.ProduceAsync((object)Guid.NewGuid().ToString(),
            message, new MessageHeaders(new Headers()
            {
                {
                    "trigger", System.Text.Encoding.UTF8.GetBytes(trigger)
                }
            }));
    }
}