namespace Solution.Core.Entities;

public class Configuration : Entity
{
    public int MinSpread { get; private set; }

    public Configuration(int minSpread)
    {
        Id = Guid.NewGuid();

        MinSpread = minSpread;
        
        RegisterDate = DateTime.Now;
        UpdateDate = DateTime.Now;
    }
}