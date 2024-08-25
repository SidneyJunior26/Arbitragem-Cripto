using Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;
using Solution.Infrastructure.Persistence.Context;

namespace Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Implementations;

public class OpportunityRepository : IOpportunityRepository
{
    private readonly ApplicationDbContext _context;

    public OpportunityRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Opportunity opportunity)
    {
        await _context.Opportunities.AddAsync(opportunity);
    }

    public void DeleteRangeAsync(List<Opportunity> opportunities)
    {
        _context.Opportunities.RemoveRange(opportunities);
    }

    public async Task<List<Opportunity>> GetAllByCryptoIdAsync(Guid criptoId)
    {
        return await _context.Opportunities
            .Where(o => o.CoinId == criptoId)
            .ToListAsync();
    }

    public async Task<Opportunity> GetOpportunityExists(Guid cryptoId, Guid exchangeToBuyId, Guid exchangeToSellId)
    {
        return await _context.Opportunities
            .FirstOrDefaultAsync(o => o.CoinId == cryptoId && o.ExchangeToBuyId == exchangeToBuyId && o.ExchangeToSellId == exchangeToSellId);
    }

    public async Task<Opportunity> GetToEmailAsync(Guid opportunityId)
    {
        return await _context.Opportunities
            .Include(o => o.Crypto)
            .Include(o => o.ExchangeToBuy)
            .Include(o => o.ExchangeToSell)
            .SingleOrDefaultAsync(o => o.Id == opportunityId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}