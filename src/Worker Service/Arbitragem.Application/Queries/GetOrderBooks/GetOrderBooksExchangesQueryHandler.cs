using ArbitraX.Application.Commands.SaveOrderBooks;
using ArbitraX.Core.Repositories;
using ArbitraX.Infrastructure.Services.Interfaces;
using Loader.Application.Commands.SaveDolar;
using Loader.Core.Interfaces;
using Loader.Infrastructure.Kafka.Interface;
using MediatR;
using Solution.Core.Entities;

namespace ArbitraX.Application.Queries.GetOrderBooks;

public class GetOrderBooksExchangesQueryHandler : IRequestHandler<GetOrderBooksExchangesQuery, List<OrderBook>>
{
    private readonly ICoinRepository _coinRepository;
    private readonly IExchangeRepository _exchangeRepository;
    private readonly IAwesomeService _awesomeService;
    private readonly IKafkaProducerService _kafkaProducerService;
    private readonly IMediator _mediator;
    private readonly IEnumerable<IExchangeService> _exchangeServices;

    public GetOrderBooksExchangesQueryHandler(
        ICoinRepository coinRepository,
        IAwesomeService awesomeService,
        IExchangeRepository exchangeRepository,
        IMediator mediator,
        IKafkaProducerService kafkaProducerService,
        IEnumerable<IExchangeService> exchangeServices)
    {
        _coinRepository = coinRepository;
        _awesomeService = awesomeService;
        _exchangeRepository = exchangeRepository;
        _mediator = mediator;
        _kafkaProducerService = kafkaProducerService;
        _exchangeServices = exchangeServices;
    }

    public async Task<List<OrderBook>> Handle(GetOrderBooksExchangesQuery request, CancellationToken cancellationToken)
    {
        var orderBooks = new List<OrderBook>();
        var usdPrice = await _awesomeService.GetUsdPrice();

        var saveDolarCommand = new SaveDolarCommand(usdPrice);
        await _mediator.Send(saveDolarCommand);

        var symbolCoins = await _coinRepository.GetAll();
        var exchanges = await _exchangeRepository.GetAll();

        var orderBooksByExchange = new Dictionary<string, string>();

        foreach (var crypto in symbolCoins)
        {
            var cryptoTasks = new Dictionary<string, Task<string>>();

            foreach (var exchangeService in _exchangeServices)
            {
                var exchange = exchanges.SingleOrDefault(e => e.Name.Replace(" ", "") == exchangeService.GetType().Name.Replace("Service", ""));

                if (exchange != null)
                {
                    cryptoTasks[exchange.Name] = exchangeService.GetOrderBookByCoin(request.Limit, crypto, exchanges);
                }
            }

            try
            {
                var results = await Task.WhenAll(cryptoTasks.Values);

                int index = 0;
                foreach (var exchange in cryptoTasks.Keys)
                {
                    orderBooksByExchange[exchange] = results[index];
                    index++;
                }

                foreach (var exchangeOrderBooks in orderBooksByExchange)
                {
                    var exchangeName = exchangeOrderBooks.Key;
                    var content = exchangeOrderBooks.Value;

                    if (!string.IsNullOrEmpty(content))
                    {
                        var exchangeService = _exchangeServices.SingleOrDefault(s => s.GetType().Name.Replace("Service", "") == exchangeName.Replace(" ", ""));

                        if (exchangeService != null)
                        {
                            var exchange = exchanges.SingleOrDefault(e => e.Name == exchangeName);
                            orderBooks.AddRange(exchangeService.ParseOrderBookResponse(content, crypto, exchange, usdPrice));

                            var saveOrderBookCommand = new SaveOrderBooksCommand(orderBooks, crypto.Symbol);
                            await _mediator.Send(saveOrderBookCommand);

                            orderBooks.Clear();
                        }
                    }
                }

                await _kafkaProducerService.ProduceAsync(crypto.Id.ToString());
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                // Log or handle the error
            }
        }

        return orderBooks;
    }
}

