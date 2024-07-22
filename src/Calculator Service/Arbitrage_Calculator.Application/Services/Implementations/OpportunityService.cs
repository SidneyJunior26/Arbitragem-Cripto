using Arbitrage_Calculator.Application.Services.Interfaces;
using Arbitrage_Calculator.Application.Utils;
using Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Interfaces;
using Solution.Core.Entities;
using Solution.Core.Enum;

namespace Arbitrage_Calculator.Application.Services.Implementations;

public class OpportunityService : IOpportunityService
{
    private readonly ICoinRepository _coinRepository;
    private readonly IOrderBookRepository _orderBookRepository;
    private readonly IOpportunityRepository _opportunityRepository;
    private readonly IAdmRepository _admRepository;

    public OpportunityService(ICoinRepository coinRepository, IOrderBookRepository orderBookRepository,
        IOpportunityRepository opportunityRepository, IAdmRepository admRepository)
    {
        _coinRepository = coinRepository;
        _orderBookRepository = orderBookRepository;
        _opportunityRepository = opportunityRepository;
        _admRepository = admRepository;
    }

    public async Task GenerateOpportunities()
    {
        var admConfig = await _admRepository.GetAsync();
        
        var coins = await _coinRepository.GetAll();

        foreach (var coin in coins)
        {
            var orderBooks = await _orderBookRepository.GetOrderBooksByCoin(coin.Id);

            var lowerBuyOrderBook = orderBooks
                .Where(ob => ob.Side == Side.BUY && ob.Order == 0)
                .OrderBy(ob => ob.Value)
                .FirstOrDefault();

            var highestSellOrderBook = orderBooks
                .Where(ob => ob.Side == Side.SELL && ob.Order == 0)
                .OrderByDescending(ob => ob.Value)
                .FirstOrDefault();

            if (lowerBuyOrderBook != null && highestSellOrderBook != null)
            {
                var percentageSpread = Util.CalculatePercentage(lowerBuyOrderBook.Value, highestSellOrderBook.Value);

                if (percentageSpread >= admConfig.MinSpread)
                    await CreateOpportunity(lowerBuyOrderBook, highestSellOrderBook, percentageSpread);
            }
        }
    }

    private async Task CreateOpportunity(OrderBook lowerBuyOrderBook, OrderBook highestSellOrderBook,
        double percentageSpread)
    {
        var opportunity = new Opportunity(lowerBuyOrderBook.Value,
            highestSellOrderBook.Value,
            percentageSpread,
            lowerBuyOrderBook.CoinId,
            lowerBuyOrderBook.ExchangeId,
            highestSellOrderBook.ExchangeId);

        await _opportunityRepository.CreateAsync(opportunity);
    }
}
