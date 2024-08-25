namespace Solution.Core.Entities;

public class Crypto : Entity
{
    public string Symbol { get; private set; }
    public string Name { get; private set; }

    public bool Active { get; private set; }
    public List<CoinNetwork> CoinNetworks { get; set; }
    public List<OrderBook> OrderBooks { get; set; }
    public List<Opportunity> Opportunities { get; set; }

    public Crypto()
    {
    }

    public Crypto(string symbol, string name)
    {
        Id = Guid.NewGuid();

        Symbol = symbol;
        Name = name;
        Active = true;

        RegisterDate = DateTime.Now;
        UpdateDate = DateTime.Now;
    }

    public void Update(string symbol, string name)
    {
        Symbol = symbol;
        Name = name;

        UpdateDate = DateTime.Now;
    }

    public void ChangeStatus()
    {
        Active = !Active;
    }
}