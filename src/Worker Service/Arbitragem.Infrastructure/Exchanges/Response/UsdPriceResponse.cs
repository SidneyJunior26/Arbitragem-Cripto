namespace ArbitraX.Infrastructure.Exchanges.Response;

public class UsdPriceResponse
{
    public UsdPriceDetail USDBRL { get; set; }
}

public class UsdPriceDetail
{
    public string bid { get; set; }
}

