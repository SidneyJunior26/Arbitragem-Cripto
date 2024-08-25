using Arbitragem.Application.InputModels;
using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Interfaces;

public interface ICryptoService
{
    Task<Crypto> CreateAsync(NewCoinInputModel newCoinInputModel);
    
    Task<List<Crypto>> GetAllAsync();
    Task<Crypto> GetByIdAsync(Guid id);
    Task<Crypto> GetBySymbolAsync(string symbol);

    Task DeleteAsync(Crypto crypto);
    Task Update(Crypto crypto, UpdateCoinInputModel updateCoinInputModel);
    Task UpdateStatus(Crypto crypto);
}
