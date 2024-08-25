using Solution.Core.Entities;

namespace ArbitraX.Core.Repositories;

public interface IAdmConfigRepository
{
    Task<AdmConfiguration> GetAsync();
}