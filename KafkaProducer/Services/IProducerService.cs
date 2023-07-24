using KafkaProducer.Models;

namespace KafkaProducer.Services;

public interface IProducerService
{
    Task PublishAsync(string trigger, byte[] message);
}