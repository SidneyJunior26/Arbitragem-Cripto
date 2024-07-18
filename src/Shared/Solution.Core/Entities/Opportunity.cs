using System.ComponentModel.DataAnnotations;

namespace Solution.Core.Entities;

public class Opportunity : Entity
{
    public decimal LowerValue
    {
        get;
        private set;
    }
    public decimal HighestValue
    {
        get;
        private set;
    }
    public decimal DifferencePercentage
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


    public Opportunity(decimal lowerValue, decimal highestValue, decimal differencePercentage, Guid coinId,
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

    public void Update(decimal lowerValue, decimal highestValue, decimal differencePercentage, Guid coinId,
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

