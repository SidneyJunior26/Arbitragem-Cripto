namespace Arbitragem.Application.InputModels;

public record NewCoinInputModel(string Symbol, string Name);

public record UpdateCoinInputModel(string Symbol, string Name);