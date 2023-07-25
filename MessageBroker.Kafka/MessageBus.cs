using System.Text;
using Confluent.Kafka;

namespace MessageBroker.Kafka;

public sealed class MessageBus : IDisposable
{
    private readonly IProducer<Null, string> _producer;
    private IConsumer<Null, string> _consumer;

    public MessageBus() : this("localhost") { }

    public MessageBus(string host)
    {
        var producerConfig = new ProducerConfig()
        {
            BootstrapServers = "host.docker.internal:29092"
        };
       
        

        _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    public void SendMessage(string topic, Message<Null, string> message)
    {
        _producer.ProduceAsync(topic, message);

        _producer.Flush(TimeSpan.FromSeconds(10));
    }

    // public void SubscribeOnTopic<T>(string topic, Action<T> action, CancellationToken cancellationToken) where T: class
    // {
    //     var msgBus = new MessageBus();
    //     using (msgBus._consumer = new Consumer<Null, string>(_consumerConfig, null, new StringDeserializer(Encoding.UTF8)))
    //     {
    //         msgBus._consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(topic, 0, -1) });
    //
    //         while (true)
    //         {
    //             if (cancellationToken.IsCancellationRequested)
    //                 break;
    //
    //             Message<Null, string> msg;
    //             if (msgBus._consumer.Consume(out msg, TimeSpan.FromMilliseconds(10)))
    //             {
    //                 action(msg.Value as T);
    //             }
    //         }
    //     }
    // }

    public void Dispose()
    {
        _producer?.Dispose();
        _consumer?.Dispose();
    }
}