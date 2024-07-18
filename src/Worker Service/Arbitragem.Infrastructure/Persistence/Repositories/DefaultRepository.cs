using ArbitraX.Core.Repositories;
using Solution.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ArbitraX.Infrastructure.Persistence.Repositories;

public class DefaultRepository<T> : IDefaultRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public DefaultRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<T> entity)
    {
        await _dbSet.AddRangeAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateRangeAsync(List<T> entity)
    {
        _dbSet.UpdateRange(entity);
        await _context.SaveChangesAsync();
    }
}

