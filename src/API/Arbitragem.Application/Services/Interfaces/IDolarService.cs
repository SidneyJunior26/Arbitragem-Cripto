using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Interfaces;

public interface IDolarService
{
    Task<Dolar> GetAsync();
}