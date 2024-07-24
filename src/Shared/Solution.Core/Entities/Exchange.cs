namespace Solution.Core.Entities;

public class Exchange : Entity
{
    public string Name { get; private set; }

    public string ApiUrl { get; private set; }
    public string ApiKey { get; private set; }
    public string ApiSecretKey { get; private set; }
    public List<Opportunity> OpportunitiesToBuy { get; set; }
    public List<Opportunity> OpportunitiesToSell { get; set; }
    public List<OrderBook> OrderBooks { get; set; }

    public Exchange(string name, string apiUrl, string apiKey, string apiSecretKey)
    {
        Id = Guid.NewGuid();

        Name = name;
        ApiUrl = apiUrl;
        ApiKey = apiKey;
        ApiSecretKey = apiSecretKey;

        RegisterDate = DateTime.Now;
        UpdateDate = DateTime.Now;
    }

    public void Update(string name, string apiUrl, string apiKey, string apiSecretKey)
    {
        Name = name;
        ApiUrl = apiUrl;
        ApiKey = apiKey;
        ApiSecretKey = apiSecretKey;

        UpdateDate = DateTime.Now;
    }
}