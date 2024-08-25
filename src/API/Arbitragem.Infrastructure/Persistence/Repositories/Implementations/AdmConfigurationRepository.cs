using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;
using Solution.Infrastructure.Persistence.Context;

namespace Arbitragem.Infrastructure.Persistence.Repositories.Implementations;

public class AdmConfigurationRepository : IAdmConfigurationRepository
{
    private readonly ApplicationDbContext _context;

    public AdmConfigurationRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<AdmConfiguration> GetAsync()
    {
        return await _context.AdmConfigurations
            .SingleOrDefaultAsync();
    }

    public async Task<AdmConfiguration> GetByIdAsync(Guid id)
    {
        return await _context.AdmConfigurations
            .SingleOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}