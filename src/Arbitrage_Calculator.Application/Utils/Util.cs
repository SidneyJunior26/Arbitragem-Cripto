namespace Arbitrage_Calculator.Application.Utils;

public class Util
{
    public static double CalculatePercentage(double lowerValue, double highestValue)
    {
        return ((highestValue - lowerValue) / lowerValue) * 100;
    }
}