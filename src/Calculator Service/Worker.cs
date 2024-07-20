using Arbitrage_Calculator.Application.Services.Interfaces;

namespace Arbitrage_Calculator;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IOpportunityService _opportunityService;

    public Worker(ILogger<Worker> logger, IOpportunityService opportunityService)
    {
        _logger = logger;
        _opportunityService = opportunityService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await _opportunityService.GenerateOpportunities();
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}

