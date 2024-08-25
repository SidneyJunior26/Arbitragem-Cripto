using Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;
using Solution.Infrastructure.Persistence.Context;

namespace Arbitrage_Calculator.Infrastructure.Persistence.Repositories.Implementations;

public class AdmRepository : IAdmRepository
{
    private readonly ApplicationDbContext _context;

    public AdmRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AdmConfiguration> GetAsync()
    {
        return await _context.AdmConfigurations
            .FirstOrDefaultAsync();
    }
}