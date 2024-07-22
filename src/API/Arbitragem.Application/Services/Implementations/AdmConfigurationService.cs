using Arbitragem.Application.InputModels;
using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Implementations;

public class AdmConfigurationService : IAdmConfigurationService
{
    private readonly IAdmConfigurationRepository _admConfigurationRepository;

    public AdmConfigurationService(IAdmConfigurationRepository admConfigurationRepository)
    {
        _admConfigurationRepository = admConfigurationRepository;
    }

    public async Task<AdmConfiguration> GetAsync()
    {
        return await _admConfigurationRepository.GetAsync();
    }

    public async Task<AdmConfiguration> GetByIdAsync(Guid id)
    {
        return await _admConfigurationRepository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(AdmConfiguration admConfiguration, ConfigurationInputModel configurationInputModel)
    {
        admConfiguration.Update(configurationInputModel.MinSpread);

        await _admConfigurationRepository.SaveChangesAsync();
    }
}