namespace Solution.Core.Entities;

public class Network : Entity
{
    public string Name
    {
        get;
        private set;
    }

    public List<CoinNetwork> CoinNetworks
    {
        get;
        private set;
    }

    public Network(string name)
    {
        Name = name;

        RegisterDate = DateTime.Now;
        UpdateDate = DateTime.Now;
    }

    public void Update(string name)
    {
        Name = name;

        UpdateDate = DateTime.Now;
    }
}

