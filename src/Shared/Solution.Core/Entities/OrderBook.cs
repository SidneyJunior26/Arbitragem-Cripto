﻿using Solution.Core.Enum;

namespace Solution.Core.Entities;

public class OrderBook : Entity
{
    public Side Side { get; private set; }
    public int Order { get; private set; }
    public double Value { get; private set; }
    public double Amount { get; private set; }
    public double TotalValue { get; private set; }
    public double Total { get; private set; }
    public Guid CoinId { get; private set; }
    public Crypto Crypto { get; set; }
    public Guid ExchangeId { get; private set; }
    public Exchange Exchange { get; set; }
    public List<Opportunity> Opportunities { get; set; }

    public OrderBook(Side side, int order, double value, double amount, Guid exchangeId, Guid coinId, double total)
    {
        Id = Guid.NewGuid();

        Side = side;
        Order = order;
        Value = value;
        Amount = amount;
        TotalValue = Value * Amount;
        Total = total;
        ExchangeId = exchangeId;
        CoinId = coinId;

        RegisterDate = DateTime.Now;
        UpdateDate = DateTime.Now;
    }
}