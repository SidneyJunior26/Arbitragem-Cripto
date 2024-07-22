using Arbitragem.Application.InputModels;
using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Implementations;

public class CoinService : ICoinService
{
    private readonly ICoinRepository _coinRepository;

    public CoinService(ICoinRepository coinRepository)
    {
        _coinRepository = coinRepository;
    }

    public async Task<Coin> CreateAsync(NewCoinInputModel newCoinInputModel)
    {
        var coin = new Coin(newCoinInputModel.Symbol, newCoinInputModel.Name);

        await _coinRepository.CreateAsync(coin);
        await _coinRepository.SaveChangesAsync();

        return coin;
    }

    public async Task<List<Coin>> GetAllAsync()
    {
        return await _coinRepository.GetAllAsync();
    }

    public async Task<Coin> GetByIdAsync(Guid id)
    {
        return await _coinRepository.GetByIdAsync(id);
    }

    public async Task<Coin> GetBySymbolAsync(string symbol)
    {
        return await _coinRepository.GetBySymbolAsync(symbol);
    }

    public async Task DeleteAsync(Coin coin)
    {
        _coinRepository.Delete(coin);

        await _coinRepository.SaveChangesAsync();
    }

    public async Task UpdateCoin(Coin coin, UpdateCoinInputModel updateCoinInputModel)
    { 
        coin.Update(updateCoinInputModel.Symbol, updateCoinInputModel.Name);

        await _coinRepository.SaveChangesAsync();
    }
}