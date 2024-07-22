using Arbitragem.Application.InputModels;
using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Interfaces;

public interface IAdmConfigurationService
{
    Task<AdmConfiguration> GetAsync();
    Task<AdmConfiguration> GetByIdAsync(Guid id);

    Task UpdateAsync(AdmConfiguration admConfiguration, ConfigurationInputModel configurationInputModel);
}