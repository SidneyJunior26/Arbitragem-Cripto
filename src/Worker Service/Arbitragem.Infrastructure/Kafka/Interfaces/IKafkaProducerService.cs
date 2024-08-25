namespace Loader.Infrastructure.Kafka.Interface;

public interface IKafkaProducerService
{
    Task ProduceAsync(string message);
}