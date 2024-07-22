using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Interfaces;

public interface IOpportunityService
{
    Task<List<Opportunity>> GetAllAsync();
    Task<Opportunity> GetByIdAsync(Guid id);
}