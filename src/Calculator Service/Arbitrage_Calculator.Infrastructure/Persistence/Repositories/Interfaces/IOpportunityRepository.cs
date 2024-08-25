using Solution.Core.Entities;

namespace Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Interfaces;

public interface IOpportunityRepository
{
    Task<List<Opportunity>> GetAllByCryptoIdAsync(Guid criptoId);
    Task<Opportunity> GetOpportunityExists(Guid criptoId, Guid exchangeToBuyId, Guid exchangeToSellId);
    Task<Opportunity> GetToEmailAsync(Guid opportunityId);
    void DeleteRangeAsync(List<Opportunity> opportunities);
    Task CreateAsync(Opportunity opportunity);

    Task SaveChangesAsync();
}