using System.Globalization;
using Loader.Core.Services;
using Newtonsoft.Json.Linq;
using Solution.Core.Entities;
using Solution.Core.Enum;

namespace Loader.Infrastructure.Services;

public class OkxService : ExchangeServiceBase
{
    private double? _usdPrice;

    protected override string BuildRequestUri(int limit, Crypto crypto, List<Exchange> exchanges)
    {
        var okx = exchanges.SingleOrDefault(e => e.Name == "Okx");

        var endpoint = $"{okx.ApiUrl}/market/books-full";
        var queryString = $"instId={crypto.Symbol}-USDT&sz={limit}";

        return $"{endpoint}?{queryString}";
    }

    public override List<OrderBook> ParseOrderBookResponse(string content, Crypto crypto, Exchange exchange, double? usdPrice)
    {
        _usdPrice = usdPrice;
        
        var jObject = JObject.Parse(content);

        if (jObject["code"].ToString() != "0")
            return new List<OrderBook>();

        var orderBooks = new List<OrderBook>();
        int index = 0;
        var total = 0.0;
        foreach (var bid in jObject["data"][0]["bids"])
        {
            var price = ConvertPriceToBrl(double.Parse(bid[0].ToString(), CultureInfo.InvariantCulture));

            var quantity = double.Parse(bid[1].ToString(), CultureInfo.InvariantCulture);
            
            total += price * quantity;

            orderBooks.Add(new OrderBook(Side.BUY, index, price, quantity, exchange.Id, crypto.Id, total));

            index++;
        }

        index = 0;
        total = 0.0;
        foreach (var ask in jObject["data"][0]["asks"])
        {
            var price = ConvertPriceToBrl(double.Parse(ask[0].ToString(), CultureInfo.InvariantCulture));

            var quantity = double.Parse(ask[1].ToString(), CultureInfo.InvariantCulture);
            
            total += price * quantity;

            orderBooks.Add(new OrderBook(Side.SELL, index, price, quantity, exchange.Id, crypto.Id, total));

            index++;
        }

        return orderBooks;
    }

    protected override double ConvertPriceToBrl(double value)
    {
        return (double)(value * _usdPrice);
    }
}