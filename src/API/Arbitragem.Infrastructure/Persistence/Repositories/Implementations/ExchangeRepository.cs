using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;
using Solution.Infrastructure.Persistence.Context;

namespace Arbitragem.Infrastructure.Persistence.Repositories.Implementations;

public class ExchangeRepository : IExchangeRepository
{
    private readonly ApplicationDbContext _context;

    public ExchangeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Exchange exchange)
    {
        await _context.Exchanges.AddAsync(exchange);
    }

    public async Task<List<Exchange>> GetAllAsync()
    {
        return await _context.Exchanges
            .OrderBy(e => e.Name)
            .ToListAsync();
    }

    public async Task<Exchange> GetByIdAsync(Guid id)
    {
        return await _context.Exchanges
            .SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Exchange> GetByNameAsync(string name)
    {
        return await _context.Exchanges
            .SingleOrDefaultAsync(e => e.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    public void Delete(Exchange exchange)
    {
        _context.Exchanges.Remove(exchange);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}