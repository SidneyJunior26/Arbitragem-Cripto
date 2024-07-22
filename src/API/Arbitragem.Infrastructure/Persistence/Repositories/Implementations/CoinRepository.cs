using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;
using Solution.Infrastructure.Persistence.Context;

namespace Arbitragem.Infrastructure.Persistence.Repositories.Implementations;

public class CoinRepository : RepositoryBase<ApplicationDbContext>, ICoinRepository
{
    public CoinRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task CreateAsync(Coin coin)
    {
        await _context.Coins.AddAsync(coin);
    }

    public async Task<List<Coin>> GetAllAsync()
    {
        return await _context.Coins.ToListAsync();
    }

    public async Task<Coin> GetByIdAsync(Guid id)
    {
        return await _context.Coins
            .SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Coin> GetBySymbolAsync(string symbol)
    {
        return await _context.Coins
            .SingleOrDefaultAsync(c => c.Symbol == symbol);
    }

    public void Delete(Coin coin)
    {
        _context.Coins.Remove(coin);
    }
}