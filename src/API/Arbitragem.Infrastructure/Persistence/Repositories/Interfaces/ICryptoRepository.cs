using Solution.Core.Entities;

namespace Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;

public interface ICryptoRepository
{
    Task CreateAsync(Crypto crypto);
    
    Task<List<Crypto>> GetAllAsync();
    Task<Crypto> GetByIdAsync(Guid id);
    Task<Crypto> GetBySymbolAsync(string symbol);

    void Delete(Crypto crypto);

    Task SaveChangesAsync();
}