using System.Globalization;
using Loader.Core.Services;
using Newtonsoft.Json.Linq;
using Solution.Core.Entities;
using Solution.Core.Enum;

namespace Arbitragem.Infrastructure.Services;

public class MercadoBitcoinService : ExchangeServiceBase
{
    protected override string BuildRequestUri(int limit, Crypto crypto, List<Exchange> exchanges)
    {
        var mercadoBitcoin = exchanges.SingleOrDefault(e => e.Name == "Mercado Bitcoin");

        var endpoint = $"/{crypto.Symbol}-BRL/orderbook";
        //var endpoint = $"/{crypto.Symbol}FT-BRL/orderbook";
        var queryString = $"limit={limit}";

        return $"{mercadoBitcoin.ApiUrl}{endpoint}?{queryString}";
    }

    public override List<OrderBook> ParseOrderBookResponse(string content, Crypto crypto, Exchange exchange, double? usdPrice)
    {
        var jObject = JObject.Parse(content);

        var orderBooks = new List<OrderBook>();
        int index = 0;
        var total = 0.0;
        foreach (var bid in jObject["bids"])
        {
            double price = double.Parse(bid[0].ToString(), CultureInfo.InvariantCulture);

            var quantity = double.Parse(bid[1].ToString(), CultureInfo.InvariantCulture);
            
            total += price * quantity;

            orderBooks.Add(new OrderBook(Side.BUY, index, price, quantity, exchange.Id, crypto.Id,total));

            index++;
        }

        index = 0;
        total = 0.0;
        foreach (var ask in jObject["asks"])
        {
            double price = double.Parse(ask[0].ToString(), CultureInfo.InvariantCulture);

            var quantity = double.Parse(ask[1].ToString(), CultureInfo.InvariantCulture);
            
            total += price * quantity;

            orderBooks.Add(new OrderBook(Side.SELL, index, price, quantity, exchange.Id, crypto.Id, total));

            index++;
        }
        
        return orderBooks;
    }
}