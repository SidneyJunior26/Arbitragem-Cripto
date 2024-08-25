using Solution.Core.Entities;

namespace Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;

public interface IAdmConfigurationRepository
{
    Task<AdmConfiguration> GetAsync();
    Task<AdmConfiguration> GetByIdAsync(Guid id);

    Task SaveChangesAsync();
}