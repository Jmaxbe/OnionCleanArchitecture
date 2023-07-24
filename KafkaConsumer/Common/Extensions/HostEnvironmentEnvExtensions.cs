namespace KafkaConsumer.Common.Extensions;

public static class HostEnvironmentEnvExtensions
{
    public static bool IsLocal(this IWebHostEnvironment webHostEnvironment)
    {
        return webHostEnvironment.IsEnvironment(Environments.Local);
    }
}