namespace Solution.Core.Entities;

public class Dolar : Entity
{
    public double Value { get; set; }

    public Dolar(double value)
    {
        Value = value;
    }

    public void Update(double value)
    {
        Value = value;
    }
}