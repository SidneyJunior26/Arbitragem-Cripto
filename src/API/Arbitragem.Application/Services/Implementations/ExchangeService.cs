using Arbitragem.Application.InputModels;
using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Implementations;

public class ExchangeService : IExchangeService
{
    private readonly IExchangeRepository _exchangeRepository;

    public ExchangeService(IExchangeRepository exchangeRepository)
    {
        _exchangeRepository = exchangeRepository;
    }

    public async Task<Exchange> CreateAsync(ExchangeInputModel exchangeInputModel)
    {
        var exchange = new Exchange(exchangeInputModel.Name,
            exchangeInputModel.ApiUrl,
            exchangeInputModel.ApiKey,
            exchangeInputModel.ApiSecretKey);

        await _exchangeRepository.CreateAsync(exchange);
        await _exchangeRepository.SaveChangesAsync();

        return exchange;
    }

    public async Task<List<Exchange>> GetAllAsync()
    {
        return await _exchangeRepository.GetAllAsync();
    }

    public async Task<Exchange> GetByIdAsync(Guid id)
    {
        return await _exchangeRepository.GetByIdAsync(id);
    }

    public async Task<Exchange> GetByNameAsync(string name)
    {
        return await _exchangeRepository.GetByNameAsync(name);
    }

    public async Task UpdateAsync(Exchange exchange, ExchangeInputModel exchangeInputModel)
    { 
        exchange.Update(exchangeInputModel.Name,
            exchangeInputModel.ApiUrl,
            exchangeInputModel.ApiKey,
            exchangeInputModel.ApiSecretKey);

        await _exchangeRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Exchange exchange)
    {
        _exchangeRepository.Delete(exchange);

        await _exchangeRepository.SaveChangesAsync();
    }
}