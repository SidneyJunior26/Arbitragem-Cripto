using Solution.Core.Entities;
using Solution.Core.Enum;

namespace Arbitragem.Application.Services.Interfaces;

public interface IOrderBookService
{
    Task<List<OrderBook>> GetByCryptoAndExchange(Guid cryptoId, Guid exchangeId, Side side);
}