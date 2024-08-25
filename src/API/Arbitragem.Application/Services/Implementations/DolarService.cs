using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Implementations;

public class DolarService : IDolarService
{
    private readonly IDolarRepository _dolarRepository;

    public DolarService(IDolarRepository dolarRepository)
    {
        _dolarRepository = dolarRepository;
    }

    public async Task<Dolar> GetAsync()
    {
        return await _dolarRepository.GetAsync();
    }
}