namespace Solution.Core.Entities;

public class AdmConfiguration : Entity
{
    public int MinSpread { get; private set; }

    public AdmConfiguration()
    {
    }

    public AdmConfiguration(int minSpread)
    {
        Id = Guid.NewGuid();

        MinSpread = minSpread;

        RegisterDate = DateTime.Now;
        UpdateDate = DateTime.Now;
    }

    public void Update(int minSpread)
    {
        MinSpread = minSpread;
    }
}