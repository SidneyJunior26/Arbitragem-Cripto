using ArbitraX.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;
using Solution.Infrastructure.Persistence.Context;

namespace ArbitraX.Infrastructure.Persistence.Repositories;

public class AdmConfigRepository : IAdmConfigRepository
{
    private readonly ApplicationDbContext _context;

    public AdmConfigRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AdmConfiguration> GetAsync()
    {
        return await _context.AdmConfigurations.SingleOrDefaultAsync();
    }
}