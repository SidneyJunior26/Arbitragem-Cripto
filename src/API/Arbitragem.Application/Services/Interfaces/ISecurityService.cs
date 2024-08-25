using Arbitragem.Application.InputModels;
using Solution.Core.Entities;

namespace Arbitragem.Application.Services.Interfaces;

public interface ISecurityService
{
    string Login(User user, LoginInputModel loginInputModel);
    bool ValidatePassword(User user, string password);
    string Encrypt(string input);
}