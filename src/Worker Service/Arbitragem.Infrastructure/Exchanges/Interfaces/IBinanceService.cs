namespace Arbitragem.Infrastructure.Exchanges.Interfaces;

public interface IBinanceService
{
    Task<string> GetOrderBookByCoin(string symbol, int limit);
}