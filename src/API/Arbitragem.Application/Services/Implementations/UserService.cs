using Arbitragem.Application.InputModels;
using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Infrastructure.Persistence.Repositories.Interfaces;
using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateAsync(NewUserInputModel newUserInputModel)
    {
        var user = new User(newUserInputModel.Name,
            newUserInputModel.Email,
            newUserInputModel.Password,
            newUserInputModel.Trial);

        await _userRepository.CreateAsync(user);

        return user;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task UpdateAsync(User user, UpdateUserInputModel updateUserInputModel)
    {
        user.Update(updateUserInputModel.Name,
            updateUserInputModel.Email);

        await _userRepository.SaveChangesAsync();
    }

    public async Task ChangeTrialAsync(User user)
    {
        user.ChangeTrial();

        await _userRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _userRepository.Delete(user);

        await _userRepository.SaveChangesAsync();
    }
}