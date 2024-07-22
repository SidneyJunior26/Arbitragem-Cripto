using Solution.Core.Entities;

namespace Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;

public interface IOpportunityRepository
{
    Task<List<Opportunity>> GetAllAsync();
    Task<Opportunity> GetByIdAsync(Guid id);
}