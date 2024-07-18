namespace Arbitragem.Infrastructure.Exchanges.Interfaces;

public interface ICoinGeckoService
{
    Task<HttpResponseMessage> GetCoins();
}

