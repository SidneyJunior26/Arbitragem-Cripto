using Arbitrage_Calculator.Application.Services.Interfaces;
using Arbitrage_Calculator.Infrastructure.Email;
using Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Interfaces;
using Solution.Core.Entities;
using Solution.Core.Enum;

namespace Arbitrage_Calculator.Application.Services.Implementations;

public class OpportunityService : IOpportunityService
{
    private readonly ICoinRepository _coinRepository;
    private readonly IOrderBookRepository _orderBookRepository;
    private readonly IOpportunityRepository _opportunityRepository;
    private readonly IEmailService _emailService;

    public OpportunityService(ICoinRepository coinRepository, IOrderBookRepository orderBookRepository,
        IOpportunityRepository opportunityRepository, IEmailService emailService)
    {
        _coinRepository = coinRepository;
        _orderBookRepository = orderBookRepository;
        _opportunityRepository = opportunityRepository;
        _emailService = emailService;
    }

    public async Task GenerateOpportunities(Guid coinId)
    {
        var coin = await _coinRepository.GetById(coinId);

        if (coin != null)
        {
            await DeleteOpportunitiesCrypto(coin.Id);

            var orderBooks = await _orderBookRepository.GetOrderBooksByCoin(coin.Id);

            var buyOrders = orderBooks.Where(ob => ob.Side == Side.SELL).OrderBy(ob => ob.Value).ToList();
            var sellOrders = orderBooks.Where(ob => ob.Side == Side.BUY).OrderBy(ob => ob.Value).ToList();

            foreach (var buyOrder in buyOrders)
            {
                foreach (var sellOrder in sellOrders.Where(ob => ob.ExchangeId != buyOrder.ExchangeId))
                {
                    if (sellOrder.Value > buyOrder.Value)
                    {
                        var percentageSpread = ((sellOrder.Value - buyOrder.Value) / buyOrder.Value) * 100;

                        if (percentageSpread >= 1 && sellOrder.TotalValue > 100 && buyOrder.TotalValue > 100)
                        {
                            var opportunity = await _opportunityRepository.GetOpportunityExists(coin.Id, buyOrder.ExchangeId, sellOrder.ExchangeId); 

                            if (opportunity == null)
                            {
                                var newOpportunity = await CreateOpportunity(buyOrder, sellOrder, percentageSpread);

                                if (percentageSpread >= 10)
                                {
                                    try
                                    {
                                        var opportunityToEmail = await _opportunityRepository.GetToEmailAsync(newOpportunity.Id);

                                        _emailService.SendNotificationOpportunity(opportunityToEmail);
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private async Task DeleteOpportunitiesCrypto(Guid cryptoId)
    {
        var opportunities = await _opportunityRepository.GetAllByCryptoIdAsync(cryptoId);

        _opportunityRepository.DeleteRangeAsync(opportunities);

        await _opportunityRepository.SaveChangesAsync();
    }

    private async Task<Opportunity> CreateOpportunity(OrderBook buyOrder, OrderBook sellOrder,
        double percentageSpread)
    {
        var opportunity = new Opportunity(buyOrder.Value,
            sellOrder.Value,
            percentageSpread,
            buyOrder.CoinId,
            buyOrder.ExchangeId,
            sellOrder.ExchangeId);

        await _opportunityRepository.CreateAsync(opportunity);

        await _opportunityRepository.SaveChangesAsync();

        return opportunity;
    }
}