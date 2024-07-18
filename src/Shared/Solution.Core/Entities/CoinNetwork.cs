namespace Solution.Core.Entities;

public class CoinNetwork : Entity
{
    public Guid CoinId { get; set; }
    public Coin Coin { get; set; }
    public Guid NetworkId { get; set; }
    public Network Network { get; set; }

    public CoinNetwork(Guid coinId, Guid networkId)
    {
        Id = Guid.NewGuid();

        CoinId = coinId;
        NetworkId = networkId;

        RegisterDate = DateTime.Now;
        UpdateDate = DateTime.Now;
    }

    public void Update(Guid coinId, Guid networkId)
    {
        CoinId = coinId;
        NetworkId = networkId;

        UpdateDate = DateTime.Now;
    }
}

