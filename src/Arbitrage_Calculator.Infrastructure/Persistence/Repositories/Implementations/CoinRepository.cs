using Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;
using Solution.Infrastructure.Persistence.Context;

namespace Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Implementations;

public class CoinRepository : ICoinRepository
{
    private readonly ApplicationDbContext _context;
    
    public CoinRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Coin>> GetAll()
    {
        return await _context.Coins.ToListAsync();
    }
}