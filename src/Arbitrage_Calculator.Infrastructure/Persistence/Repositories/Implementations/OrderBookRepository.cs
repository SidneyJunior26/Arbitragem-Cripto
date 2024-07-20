using Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;
using Solution.Infrastructure.Persistence.Context;

namespace Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Implementations;

public class OrderBookRepository : IOrderBookRepository
{
    private readonly ApplicationDbContext _context;

    public OrderBookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderBook>> GetOrderBooksByCoin(Guid coinId)
    {
        return await _context.OrderBooks
            .Where(ob => ob.CoinId == coinId)
            .ToListAsync();
    }
}