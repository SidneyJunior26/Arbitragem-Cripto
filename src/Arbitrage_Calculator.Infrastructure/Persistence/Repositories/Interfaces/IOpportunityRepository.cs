using Solution.Core.Entities;

namespace Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Interfaces;

public interface IOpportunityRepository
{
    Task CreateAsync(Opportunity opportunity);
}