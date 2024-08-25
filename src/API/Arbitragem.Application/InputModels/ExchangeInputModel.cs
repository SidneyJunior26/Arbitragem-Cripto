namespace Arbitragem.Application.InputModels;

public record ExchangeInputModel(string Name, string ExchangeUrl, string ApiUrl, string ApiKey, string ApiSecretKey, double Fee);