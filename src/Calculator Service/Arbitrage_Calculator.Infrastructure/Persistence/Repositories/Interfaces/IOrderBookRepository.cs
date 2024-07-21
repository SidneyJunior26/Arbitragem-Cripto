using Solution.Core.Entities;

namespace Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Interfaces;

public interface IOrderBookRepository
{
    Task<List<OrderBook>> GetOrderBooksByCoin(Guid coinId);
}