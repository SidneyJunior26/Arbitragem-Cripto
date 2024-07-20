using Solution.Core.Entities;

namespace Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Interfaces;

public interface ICoinRepository
{
    Task<List<Coin>> GetAll();
}