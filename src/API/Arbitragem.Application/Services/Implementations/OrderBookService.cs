using Arbitragem.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;
using Solution.Core.Enum;
using Solution.Infrastructure.Persistence.Context;

namespace Arbitragem.Application.Services.Implementations;

public class OrderBookService : IOrderBookService
{
    private readonly ApplicationDbContext _context;

    public OrderBookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderBook>> GetByCryptoAndExchange(Guid cryptoId, Guid exchangeId, Side side)
    {
        return await _context.OrderBooks
            .Where(ob => ob.CoinId == cryptoId && ob.ExchangeId == exchangeId && ob.Side == side)
            .OrderBy(ob => ob.Order)
            .ToListAsync();
    }
}