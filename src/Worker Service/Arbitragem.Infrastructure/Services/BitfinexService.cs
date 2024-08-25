using System.Globalization;
using Loader.Core.Services;
using Newtonsoft.Json.Linq;
using Solution.Core.Entities;
using Solution.Core.Enum;

namespace Loader.Infrastructure.Services;

public class BitfinexService : ExchangeServiceBase
{
    private double? _usdPrice;

    protected override string BuildRequestUri(int limit, Crypto crypto, List<Exchange> exchanges)
    {
        var bitfinex = exchanges.SingleOrDefault(e => e.Name == "Bitfinex");

        var endpoint = $"{bitfinex.ApiUrl}/book/{crypto.Symbol}USD";

        return $"{endpoint}";
    }

    public override List<OrderBook> ParseOrderBookResponse(string content, Crypto crypto, Exchange exchange,
        double? usdPrice)
    {
        _usdPrice = usdPrice;
        
        var jObject = JObject.Parse(content);

        var orderBooks = new List<OrderBook>();
        int index = 0;
        var total = 0.0;
        foreach (var bid in jObject["bids"])
        {
            var price = ConvertPriceToBrl(double.Parse(bid["price"].ToString(), CultureInfo.InvariantCulture));

            var quantity = double.Parse(bid["amount"].ToString(), CultureInfo.InvariantCulture);

            total += price * quantity;

            orderBooks.Add(new OrderBook(Side.BUY, index, price, quantity, exchange.Id, crypto.Id, total));
            
            if (index == 4)
                break;

            index++;
        }

        index = 0;
        total = 0.0;
        foreach (var ask in jObject["asks"])
        {
            var price = ConvertPriceToBrl(double.Parse(ask["price"].ToString(), CultureInfo.InvariantCulture));

            var quantity = double.Parse(ask["amount"].ToString(), CultureInfo.InvariantCulture);
            
            total += price * quantity;

            orderBooks.Add(new OrderBook(Side.SELL, index, price, quantity, exchange.Id, crypto.Id, total));
            
            if (index == 4)
                break;

            index++;
        }

        return orderBooks;
    }

    protected override double ConvertPriceToBrl(double value)
    {
        return (double)(value * _usdPrice);
    }
}