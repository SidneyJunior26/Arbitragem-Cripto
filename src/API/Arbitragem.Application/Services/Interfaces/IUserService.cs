using Arbitragem.Application.InputModels;
using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Interfaces;

public interface IUserService
{
    Task<User> CreateAsync(NewUserInputModel newUserInputModel);

    Task<List<User>> GetAllAsync();
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByEmailAsync(string email);

    Task UpdateAsync(User user, UpdateUserInputModel updateUserInputModel);
    Task ChangeTrialAsync(User user);

    Task DeleteAsync(User user);
}