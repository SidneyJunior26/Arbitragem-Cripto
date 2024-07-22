using Arbitragem.Application.InputModels;
using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Interfaces;

public interface IExchangeService
{
    Task<Exchange> CreateAsync(ExchangeInputModel exchangeInputModel);

    Task<List<Exchange>> GetAllAsync();
    Task<Exchange> GetByIdAsync(Guid id);
    Task<Exchange> GetByNameAsync(string name);

    Task UpdateAsync(Exchange exchange, ExchangeInputModel exchangeInputModel);

    Task DeleteAsync(Exchange exchange);
}