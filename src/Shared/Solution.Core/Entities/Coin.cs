namespace Solution.Core.Entities;

public class Coin : Entity
{
    public string Symbol
    {
        get;
        private set;
    }
    public string Name
    {
        get;
        private set;
    }
    public List<CoinNetwork> CoinNetworks
    {
        get;
        set;
    }
    public List<OrderBook> OrderBooks
    {
        get;
        set;
    }
    public List<Opportunity> Opportunities
    {
        get;
        set;
    }

    public Coin()
    {

    }

    public Coin(string symbol, string name)
    {
        Id = Guid.NewGuid();

        Symbol = symbol;
        Name = name;

        RegisterDate = DateTime.Now;
        UpdateDate = DateTime.Now;
    }
    public void Update(string symbol, string name)
    {
        Symbol = symbol;
        Name = name;

        UpdateDate = DateTime.Now;
    }
}