using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;
using Solution.Infrastructure.Persistence.Context;

namespace Arbitragem.Infrastructure.Persistence.Repositories.Implementations;

public class DolarRepository : IDolarRepository
{
    private readonly ApplicationDbContext _context;

    public DolarRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Dolar> GetAsync()
    {
        return await _context.Dolar.FirstOrDefaultAsync();
    }
}