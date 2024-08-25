using Loader.Core.Services;
using Newtonsoft.Json.Linq;
using Solution.Core.Entities;
using Solution.Core.Enum;

namespace Loader.Infrastructure.Services;

public class BitgetService : ExchangeServiceBase
{
    private double? _usdPrice;

    protected override string BuildRequestUri(int limit, Crypto crypto, List<Exchange> exchanges)
    {
        var bitget = exchanges.SingleOrDefault(e => e.Name == "Bitget");

        var endpoint = $"{bitget.ApiUrl}/spot/market/orderbook";
        var queryString = $"symbol={crypto.Symbol}USDT&type=step1&limit={limit}";

        return $"{endpoint}?{queryString}";
    }

    public override List<OrderBook> ParseOrderBookResponse(string content, Crypto crypto, Exchange exchange,
        double? usdPrice)
    {
        _usdPrice = usdPrice;
        
        var jObject = JObject.Parse(content);

        if (jObject["code"].ToString() != "00000")
            return new List<OrderBook>();

        var orderBooks = new List<OrderBook>();
        int index = 0;
        var total = 0.0;
        foreach (var bid in jObject["data"]["bids"])
        {
            var price = ConvertPriceToBrl(double.Parse(bid[0].ToString()));

            var quantity = double.Parse(bid[1].ToString());
            
            total += price * quantity;

            orderBooks.Add(new OrderBook(Side.BUY, index, price, quantity, exchange.Id, crypto.Id, total));
            
            if (index == 4)
                break;
            
            index++;
        }

        index = 0;
        total = 0.0;
        foreach (var ask in jObject["data"]["asks"])
        {
            var price = ConvertPriceToBrl(double.Parse(ask[0].ToString()));

            var quantity = double.Parse(ask[1].ToString());
            
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