using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Arbitragem.Infrastructure.Persistence.Repositories.Implementations;

public abstract class RepositoryBase<TContext> : IRepositoryBase where TContext : DbContext
{
    protected readonly TContext _context;

    protected RepositoryBase(TContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}