using KafkaFlow.Consumers;

namespace KafkaConsumer.Services;

public class ConsumerService : IConsumerService
{
    private readonly IConsumerAccessor _consumerAccessor;

    public ConsumerService(IConsumerAccessor consumerAccessor)
    {
        _consumerAccessor = consumerAccessor;
    }

    public async Task Test()
    {
        

    }
}