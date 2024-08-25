using ArbitraX.Application.Queries.GetOrderBooks;
using MediatR;
using Solution.Core.Entities;

namespace ArbitraX.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                var getOrderBooksQuery = new GetOrderBooksExchangesQuery(5);
                List<OrderBook> orderBooksExchanges = new List<OrderBook>();

                _logger.LogInformation("Getting Exchanges Order Books: {time}", DateTimeOffset.Now);

                try
                {
                    orderBooksExchanges = await mediator.Send(getOrderBooksQuery);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error during get orderBooks - {ex.Message}");
                }

                _logger.LogInformation("Order Books amount: {qtde} - {time}", orderBooksExchanges.Count, DateTimeOffset.Now);

                //if (orderBooksExchanges.Count > 0)
                //{
                //    _logger.LogInformation("Saving Order Books: {time}", DateTimeOffset.Now);
                //    var saveOrderBooksCommand = new SaveOrderBooksCommand(orderBooksExchanges);

                //    await mediator.Send(saveOrderBooksCommand);
                //}
            }

            await Task.Delay(15000, stoppingToken);
        }
    }
}

