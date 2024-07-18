using Arbitragem.Infrastructure.Exchanges.Interfaces;
using ArbitraX.Application.Commands.SaveOrderBooks;
using ArbitraX.Core.Repositories;
using ArbitraX.Infrastructure.Exchanges.Interfaces;
using MediatR;
using Newtonsoft.Json.Linq;
using Solution.Core.Entities;
using Solution.Core.Enum;

namespace ArbitraX.Application.Queries.GetOrderBooks;

public class GetOrderBooksQueryHandler : IRequestHandler<GetOrderBooksQuery, List<OrderBook>>
{
    private readonly ICoinRepository _coinRepository;
    private readonly IBinanceService _binanceService;
    private readonly IMercadoBitcoinService _mercadoBitcoinService;
    private readonly IExchangeRepository _exchangeRepository;
    private readonly IAwesomeService _awesomeService;

    private readonly IMediator _mediator;

    List<Exchange> exchanges = new List<Exchange>();

    private double usdPrice;

    public GetOrderBooksQueryHandler(IBinanceService binanceService, ICoinRepository coinRepository, IMercadoBitcoinService mercadoBitcoinService, IAwesomeService awesomeService, IExchangeRepository exchangeRepository, IMediator mediator)
    {
        _binanceService = binanceService;
        _coinRepository = coinRepository;
        _mercadoBitcoinService = mercadoBitcoinService;
        _awesomeService = awesomeService;
        _exchangeRepository = exchangeRepository;
        _mediator = mediator;
    }

    public async Task<List<OrderBook>> Handle(GetOrderBooksQuery request, CancellationToken cancellationToken)
    {
        var orderBooks = new List<OrderBook>();

        usdPrice = await _awesomeService.GetUsdPrice();

        this.exchanges = await _exchangeRepository.GetAll();

        var symbolCoins = await _coinRepository.GetAll();

        foreach (var symbol in symbolCoins)
        {
            var tasks = AddTasks(symbol, request.Limit);

            // Espera até que todas as tarefas para o símbolo atual sejam concluídas
            await Task.WhenAll(tasks);
        }

        return orderBooks;
    }

    private List<Task> AddTasks(Coin coin, int limit)
    {
        var tasks = new List<Task>
        {
            // Task to get data from Binance
            Task.Run(async () =>
            {
                var binanceOrderBooks = await _binanceService.GetOrderBookByCoin(coin.Symbol, limit);

                if (!string.IsNullOrEmpty(binanceOrderBooks))
                {
                    await ParseOrderBookResponse(binanceOrderBooks, coin, "Binance", false);
                }
            }),

            // Task to get data from Mercado Bitcoin
            Task.Run(async () =>
            {
                var mercadoBitcoinOrderBooks = await _mercadoBitcoinService.GetOrderBookByCoin(coin.Symbol, limit);

                if (!string.IsNullOrEmpty(mercadoBitcoinOrderBooks))
                {
                    await ParseOrderBookResponse(mercadoBitcoinOrderBooks, coin, "Mercado Bitcoin", true);
                }
            })
        };

        return tasks;
    }

    private async Task ParseOrderBookResponse(string content, Coin coin, string exchangeName, bool convertToUsd)
    {
        var exchangeId = this.exchanges.SingleOrDefault(e => e.Name == exchangeName).Id;

        var jObject = JObject.Parse(content);

        var orderBooks = new List<OrderBook>();
        foreach (var bid in jObject["bids"])
        {
            double price;
            if (convertToUsd)
                price = await ConvertPriceToUsd(double.Parse(bid[0].ToString()));
            else
                price = double.Parse(bid[0].ToString());

            var quantity = double.Parse(bid[1].ToString());

            orderBooks.Add(new OrderBook(Side.BUY, price, quantity, exchangeId, coin.Id));
        }

        foreach (var ask in jObject["asks"])
        {
            double price;

            if (convertToUsd)
                price = await ConvertPriceToUsd(double.Parse(ask[0].ToString()));
            else
                price = double.Parse(ask[0].ToString());

            var quantity = double.Parse(ask[1].ToString());

            orderBooks.Add(new OrderBook(Side.SELL, price, quantity, exchangeId, coin.Id));
        }

        var saveOrderBooksCommand = new SaveOrderBooksCommand(orderBooks);

        await _mediator.Send(saveOrderBooksCommand);
    }

    private async Task<double> ConvertPriceToUsd(double value)
    {
        return value * usdPrice;
    }
}

