using KafkaFlow;

namespace KafkaConsumer.Middleware;

public class ProcessMessagesMiddleware : IMessageMiddleware
{
    public async Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
        await Task.Delay(1000); // simulate a message processing
        await Console.Out.WriteLineAsync($"{context.ConsumerContext.ConsumerName} messaged processed");
    }
}