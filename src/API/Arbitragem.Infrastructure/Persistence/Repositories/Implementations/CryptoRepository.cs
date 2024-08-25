using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;
using Solution.Infrastructure.Persistence.Context;

namespace Arbitragem.Infrastructure.Persistence.Repositories.Implementations;

public class CryptoRepository : ICryptoRepository
{
    private readonly ApplicationDbContext _context;

    public CryptoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Crypto crypto)
    {
        await _context.Cryptos.AddAsync(crypto);
    }

    public async Task<List<Crypto>> GetAllAsync()
    {
        return await _context.Cryptos
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Crypto> GetByIdAsync(Guid id)
    {
        return await _context.Cryptos
            .SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Crypto> GetBySymbolAsync(string symbol)
    {
        return await _context.Cryptos
            .SingleOrDefaultAsync(c => c.Symbol == symbol);
    }

    public void Delete(Crypto crypto)
    {
        _context.Cryptos.Remove(crypto);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}