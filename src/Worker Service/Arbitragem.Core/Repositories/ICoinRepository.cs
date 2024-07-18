using Solution.Core.Entities;

namespace ArbitraX.Core.Repositories;

public interface ICoinRepository
{
    Task<List<Coin>> GetAll();
}