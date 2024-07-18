using ArbitraX.Core.Repositories;
using Solution.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;

namespace ArbitraX.Infrastructure.Persistence.Repositories;

public class ExchangeRepository : IExchangeRepository
{
    private readonly ApplicationDbContext _context;

    public ExchangeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Exchange>> GetAll()
    {
        return _context.Exchanges.ToListAsync();
    }

    public async Task<Guid> GetById(string name)
    {
        var exchange = await _context.Exchanges.SingleOrDefaultAsync(e => e.Name == name);

        return exchange.Id;
    }
}

