using Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Interfaces;
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
        await _context.Opportunities.AddRangeAsync(opportunity);

        await _context.SaveChangesAsync();
    }
}