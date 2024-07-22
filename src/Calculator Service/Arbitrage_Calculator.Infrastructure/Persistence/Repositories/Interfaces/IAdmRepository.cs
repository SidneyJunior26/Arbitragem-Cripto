using Solution.Core.Entities;

namespace Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Interfaces;

public interface IAdmRepository
{
    Task<AdmConfiguration> GetAsync();
}