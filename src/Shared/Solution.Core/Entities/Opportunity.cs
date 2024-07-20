using System.ComponentModel.DataAnnotations;

namespace Solution.Core.Entities;

public class Opportunity : Entity
{
    public double LowerValue
    {
        get;
        private set;
    }
    public double HighestValue
    {
        get;
        private set;
    }
    public double DifferencePercentage
    {
        get;
        private set;
    }
    public Guid CoinId
    {
        get;
        private set;
    }
    public Coin Coin
    {
        get;
        private set;
    }
    public Guid ExchangeToBuyId
    {
        get;
        private set;
    }
    public Exchange ExchangeToBuy
    {
        get;
        set;
    }
    public Guid ExchangeToSellId
    {
        get;
        private set;
    }
    public Exchange ExchangeToSell
    {
        get;
        set;
    }


    public Opportunity(double lowerValue, double highestValue, double differencePercentage, Guid coinId,
        Guid exchangeToBuyId, Guid exchangeToSellId)
    {
        Id = Guid.NewGuid();

        LowerValue = lowerValue;
        HighestValue = highestValue;
        DifferencePercentage = differencePercentage;
        CoinId = coinId;
        ExchangeToBuyId = exchangeToBuyId;
        ExchangeToSellId = exchangeToSellId;

        RegisterDate = DateTime.Now;
        UpdateDate = DateTime.Now;
    }

    public void Update(double lowerValue, double highestValue, double differencePercentage, Guid coinId,
        Guid exchangeToBuyId, Guid exchangeToSellId)
    {
        LowerValue = lowerValue;
        HighestValue = highestValue;
        DifferencePercentage = differencePercentage;
        CoinId = coinId;
        ExchangeToBuyId = exchangeToBuyId;
        ExchangeToSellId = exchangeToSellId;

        UpdateDate = DateTime.Now;
    }
}

