namespace Arbitragem.Application.InputModels;

public record NewUserInputModel(string Name, string Email, string Password, bool Trial);

public record UpdateUserInputModel(string Name, string Email, bool Trial);