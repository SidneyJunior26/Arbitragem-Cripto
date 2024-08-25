using Arbitragem.Application.InputModels;
using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Implementations;

public class CryptoService : ICryptoService
{
    private readonly ICryptoRepository _cryptoRepository;

    public CryptoService(ICryptoRepository cryptoRepository)
    {
        _cryptoRepository = cryptoRepository;
    }

    public async Task<Crypto> CreateAsync(NewCoinInputModel newCoinInputModel)
    {
        var coin = new Crypto(newCoinInputModel.Symbol, newCoinInputModel.Name);

        await _cryptoRepository.CreateAsync(coin);
        await _cryptoRepository.SaveChangesAsync();

        return coin;
    }

    public async Task<List<Crypto>> GetAllAsync()
    {
        return await _cryptoRepository.GetAllAsync();
    }

    public async Task<Crypto> GetByIdAsync(Guid id)
    {
        return await _cryptoRepository.GetByIdAsync(id);
    }

    public async Task<Crypto> GetBySymbolAsync(string symbol)
    {
        return await _cryptoRepository.GetBySymbolAsync(symbol);
    }

    public async Task DeleteAsync(Crypto crypto)
    {
        _cryptoRepository.Delete(crypto);

        await _cryptoRepository.SaveChangesAsync();
    }

    public async Task Update(Crypto crypto, UpdateCoinInputModel updateCoinInputModel)
    { 
        crypto.Update(updateCoinInputModel.Symbol, updateCoinInputModel.Name);

        await _cryptoRepository.SaveChangesAsync();
    }

    public async Task UpdateStatus(Crypto crypto)
    {
        crypto.ChangeStatus();
        
        await _cryptoRepository.SaveChangesAsync();
    }
}