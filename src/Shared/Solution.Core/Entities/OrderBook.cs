using Solution.Core.Enum;

namespace Solution.Core.Entities;

public class OrderBook : Entity
{
    public Side Side
    {
        get;
        private set;
    }
    public double Value
    {
        get;
        private set;
    }
    public double Amount
    {
        get;
        private set;
    }
    public double TotalValue
    {
        get;
        private set;
    }
    public Guid CoinId
    {
        get;
        private set;
    }
    public Coin Coin { get; set; }
    public Guid ExchangeId
    {
        get;
        private set;
    }
    public Exchange Exchange { get; set; }
    public List<Opportunity> Opportunities { get; set; }

    public OrderBook(Side side, double value, double amount, Guid exchangeId, Guid coinId)
    {
        Id = Guid.NewGuid();

        Side = side;
        Value = value;
        Amount = amount;
        TotalValue = Value * Amount;
        ExchangeId = exchangeId;
        CoinId = coinId;

        RegisterDate = DateTime.Now;
        UpdateDate = DateTime.Now;
    }

    public void Update(Side side, double value, double amount, Guid exchangeId, Guid coinId)
    {
        Side = side;
        Value = value;
        Amount = amount;
        TotalValue = Value * Amount;
        ExchangeId = exchangeId;
        CoinId = coinId;

        UpdateDate = DateTime.Now;
    }
}

