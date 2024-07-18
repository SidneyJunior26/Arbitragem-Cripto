namespace ArbitraX.Core.Repositories;

public interface IDefaultRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task AddRangeAsync(List<T> entity);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(List<T> entity);
    Task DeleteAsync(T entity);
    Task SaveChangesAsync();
}