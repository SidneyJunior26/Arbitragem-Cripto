namespace Solution.Core.Entities;

public class Exchange : Entity
{
    public string Name
    {
        get;
        private set;
    }
    public List<Opportunity> Opportunities { get; set; }
    public List<OrderBook> OrderBooks { get; set; }

    public Exchange()
    {

    }

    public Exchange(string name)
    {
        Id = Guid.NewGuid();

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