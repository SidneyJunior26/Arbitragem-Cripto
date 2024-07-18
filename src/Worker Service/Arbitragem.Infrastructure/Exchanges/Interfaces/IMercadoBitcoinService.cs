namespace Arbitragem.Infrastructure.Exchanges.Interfaces;

public interface IMercadoBitcoinService
{
    Task<string> GetOrderBookByCoin(string symbol, int limit);
}