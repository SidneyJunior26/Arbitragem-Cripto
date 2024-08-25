using Solution.Core.Entities;

namespace Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;

public interface IDolarRepository
{
    Task<Dolar> GetAsync();
}