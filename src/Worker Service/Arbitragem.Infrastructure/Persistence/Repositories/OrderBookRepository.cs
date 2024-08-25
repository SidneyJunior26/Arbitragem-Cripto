using ArbitraX.Core.Repositories;
using Solution.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;
using Solution.Core.Enum;

namespace ArbitraX.Infrastructure.Persistence.Repositories;

public class OrderBookRepository : IOrderBookRepository
{
    private readonly ApplicationDbContext _context;

    public OrderBookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void DeleteOrdersBooksList(List<OrderBook> orderBooks)
    {
        _context.RemoveRange(orderBooks);
    }

    public async Task DeleteOrderOrderBooksAsync()
    {
        var orderBooks = await _context.OrderBooks.ToListAsync();

        _context.RemoveRange(orderBooks);
    }

    public Task<List<OrderBook>> GetAll(string coins)
    {
        throw new NotImplementedException();
    }

    public async Task<List<OrderBook>> GetListBySymbolsAndExchangeAndSide(string symbol, Guid exchangeId)
    {
        return await _context.OrderBooks
            .Where(o => o.Crypto.Symbol == symbol && o.ExchangeId == exchangeId)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task CreateOrderBookAsync(OrderBook orderBook)
    {
        await _context.OrderBooks.AddAsync(orderBook);
    }

    public async Task CreateOrderBooksListAsync(List<OrderBook> orderBooks)
    {
        await _context.OrderBooks.AddRangeAsync(orderBooks);
    }

    public void UpdateOrderBooksAsync(List<OrderBook> orderBooks)
    {
        _context.OrderBooks.UpdateRange(orderBooks);
    }
}

