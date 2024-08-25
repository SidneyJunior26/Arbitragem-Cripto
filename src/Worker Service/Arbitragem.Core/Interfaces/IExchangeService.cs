using Solution.Core.Entities;

namespace Loader.Core.Interfaces;

public interface IExchangeService
{
    Task<string> GetOrderBookByCoin(int limit, Crypto crypto, List<Exchange> exchanges);
    List<OrderBook> ParseOrderBookResponse(string content, Crypto crypto, Exchange exchange, double? usdPrice);
}
