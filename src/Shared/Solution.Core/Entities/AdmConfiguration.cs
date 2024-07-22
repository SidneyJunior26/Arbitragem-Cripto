namespace Solution.Core.Entities;

public class AdmConfiguration : Entity
{
    public int MinSpread { get; private set; }

    public AdmConfiguration()
    {
    }

    public void Update(int minSpread)
    {
        MinSpread = minSpread;
    }
}