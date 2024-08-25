namespace Arbitragem.Application.ViewModels;

public record OpportunityViewModel(
    int Position,
    double ValueToBuy,
    double ValueToSell,
    double DifferencePercentage,
    Guid CryptoId,
    string Symbol,
    string Name,
    Guid ExchangeToBuyId,
    string ExchangeToBuyName,
    Guid ExchangeToSellId,
    string ExchangeToSellName,
    DateTime Date,
    string ExchangeToBuyUrl,
    string ExchangeToSellUrl,
    double Fee
    );