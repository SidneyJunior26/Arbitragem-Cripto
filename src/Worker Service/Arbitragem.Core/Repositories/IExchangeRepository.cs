using Solution.Core.Entities;

namespace ArbitraX.Core.Repositories;

public interface IExchangeRepository
{
    Task<List<Exchange>> GetAll();
    Task<Exchange> GetByNameAsync(string name);
}

