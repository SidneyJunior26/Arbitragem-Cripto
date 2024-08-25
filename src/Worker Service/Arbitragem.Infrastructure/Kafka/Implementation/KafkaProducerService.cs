using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Loader.Infrastructure.Kafka.Interface;

namespace Loader.Infrastructure.Kafka.Implementation;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<KafkaProducerService> _logger;
    
    private readonly string kafkaBootstrapServer;
    private readonly string kafkaTopic;

    public KafkaProducerService(ILogger<KafkaProducerService> logger, IConfiguration config)
    {
        kafkaBootstrapServer = config["Kafka:BootstrapServer"];
        kafkaTopic = config["Kafka:Topic"];

        var kafkaConfig = new ProducerConfig
        {
            BootstrapServers = kafkaBootstrapServer
        };

        _producer = new ProducerBuilder<Null, string>(kafkaConfig).Build();
        _logger = logger;
    }

    public async Task ProduceAsync(string message)
    {
        try
        {
            var result = await _producer.ProduceAsync(kafkaTopic, new Message<Null, string> { Value = message });

            _logger.LogInformation($"Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");
        }
        catch (ProduceException<Null, string> e)
        {
            _logger.LogError($"Delivery failed: {e.Error.Reason}");
        }
    }
}