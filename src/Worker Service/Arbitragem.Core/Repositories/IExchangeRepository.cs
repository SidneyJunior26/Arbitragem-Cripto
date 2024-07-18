using Solution.Core.Entities;

namespace ArbitraX.Core.Repositories;

public interface IExchangeRepository
{
    Task<List<Exchange>> GetAll();
    Task<Guid> GetById(string name);
}

