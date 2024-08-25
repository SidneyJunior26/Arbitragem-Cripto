using Arbitrage_Calculator.Application.Services.Interfaces;
using Confluent.Kafka;

namespace Arbitrage_Calculator;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly string _kafkaTopic;
    private readonly string _groupId;
    private readonly string _bootstrapServers;

    private readonly IServiceScopeFactory _serviceScopeFactory;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory, IConfiguration config)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;

        _bootstrapServers = config["Kafka:BootstrapServer"];
        _kafkaTopic = config["Kafka:Topic"];
        _groupId = config["Kafka:GroupId"];
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            GroupId = _groupId,
            BootstrapServers = _bootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        
        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(_kafkaTopic);
        
        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var cr = consumer.Consume(cts.Token);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var opportunityService = scope.ServiceProvider.GetRequiredService<IOpportunityService>();
                    await opportunityService.GenerateOpportunities(Guid.Parse(cr.Value));
                }
            }
            catch (OperationCanceledException)
            {
                // Ignore cancellation exceptions
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while consuming Kafka messages.");
            }
        }

        // while (!stoppingToken.IsCancellationRequested)
        // {
        //     if (_logger.IsEnabled(LogLevel.Information))
        //     {
        //         _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //     }
        //     
        //     await Task.Delay(1000, stoppingToken);
        // }
    }
}

