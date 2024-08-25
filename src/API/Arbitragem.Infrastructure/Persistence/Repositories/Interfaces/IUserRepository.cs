using Solution.Core.Entities;

namespace Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;

public interface IUserRepository
{
    Task CreateAsync(User user);
    
    Task<List<User>> GetAllAsync();
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByEmailAsync(string email);

    void Delete(User user);

    Task SaveChangesAsync();
}