namespace Solution.Core.Entities;

public class Exchange : Entity
{
    public string Name { get; private set; }
    public string ExchangeUrl { get; private set; }
    public string ApiUrl { get; private set; }
    public string ApiKey { get; private set; }
    public string ApiSecretKey { get; private set; }
    public double Fee { get; private set; }
    public List<Opportunity> OpportunitiesToBuy { get; set; }
    public List<Opportunity> OpportunitiesToSell { get; set; }
    public List<OrderBook> OrderBooks { get; set; }

    public Exchange(string name, string exchangeUrl, string apiUrl, string apiKey, string apiSecretKey, double fee)
    {
        Id = Guid.NewGuid();

        Name = name;
        ExchangeUrl = exchangeUrl;
        ApiUrl = apiUrl;
        ApiKey = apiKey;
        ApiSecretKey = apiSecretKey;
        Fee = fee;

        RegisterDate = DateTime.Now;
        UpdateDate = DateTime.Now;
    }

    public void Update(string name, string exchangeUrl, string apiUrl, string apiKey, string apiSecretKey, double fee)
    {
        Name = name;
        ExchangeUrl = exchangeUrl;
        ApiUrl = apiUrl;
        ApiKey = apiKey;
        ApiSecretKey = apiSecretKey;
        Fee = fee;

        UpdateDate = DateTime.Now;
    }
}