using Solution.Core.Entities;

namespace Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;

public interface ICoinRepository : IRepositoryBase
{
    Task CreateAsync(Coin coin);
    
    Task<List<Coin>> GetAllAsync();
    Task<Coin> GetByIdAsync(Guid id);
    Task<Coin> GetBySymbolAsync(string symbol);

    void Delete(Coin coin);
}