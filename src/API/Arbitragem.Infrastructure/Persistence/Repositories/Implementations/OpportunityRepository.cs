using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;
using Solution.Infrastructure.Persistence.Context;

namespace Arbitragem.Infrastructure.Persistence.Repositories.Implementations;

public class OpportunityRepository : IOpportunityRepository
{
    private readonly ApplicationDbContext _context;

    public OpportunityRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Opportunity>> GetAllAsync()
    {
        return await _context.Opportunities
            .OrderByDescending(o => o.DifferencePercentage)
            .Include(o => o.Crypto)
            .Include(o => o.ExchangeToBuy)
            .Include(o => o.ExchangeToSell)
            .ToListAsync();
    }

    public async Task<Opportunity> GetByIdAsync(Guid id)
    {
        return await _context.Opportunities
            .SingleOrDefaultAsync(o => o.Id == id);
    }
}