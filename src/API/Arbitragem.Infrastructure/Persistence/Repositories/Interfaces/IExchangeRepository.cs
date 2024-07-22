using Solution.Core.Entities;

namespace Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;

public interface IExchangeRepository : IRepositoryBase
{
    Task CreateAsync(Exchange exchange);

    Task<List<Exchange>> GetAllAsync();
    Task<Exchange> GetByIdAsync(Guid id);
    Task<Exchange> GetByNameAsync(string name);

    void Delete(Exchange exchange);
}