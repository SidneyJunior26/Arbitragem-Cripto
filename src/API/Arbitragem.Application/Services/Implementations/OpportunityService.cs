using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Implementations;

public class OpportunityService : IOpportunityService
{
    private readonly IOpportunityRepository _opportunityRepository;

    public OpportunityService(IOpportunityRepository opportunityRepository)
    {
        _opportunityRepository = opportunityRepository;
    }

    public async Task<List<Opportunity>> GetAllAsync()
    {
        return await _opportunityRepository.GetAllAsync();
    }

    public async Task<Opportunity> GetByIdAsync(Guid id)
    {
        return await _opportunityRepository.GetByIdAsync(id);
    }
}