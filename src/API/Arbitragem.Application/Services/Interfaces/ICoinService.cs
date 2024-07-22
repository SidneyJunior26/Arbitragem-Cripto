using Arbitragem.Application.InputModels;
using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Interfaces;

public interface ICoinService
{
    Task<Coin> CreateAsync(NewCoinInputModel newCoinInputModel);
    
    Task<List<Coin>> GetAllAsync();
    Task<Coin> GetByIdAsync(Guid id);
    Task<Coin> GetBySymbolAsync(string symbol);

    Task DeleteAsync(Coin coin);
    Task UpdateCoin(Coin coin, UpdateCoinInputModel updateCoinInputModel);
}
