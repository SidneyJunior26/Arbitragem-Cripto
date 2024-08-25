using System.Globalization;
using Loader.Core.Services;
using Newtonsoft.Json.Linq;
using Solution.Core.Entities;
using Solution.Core.Enum;

namespace Loader.Infrastructure.Services;

public class ChilizService : ExchangeServiceBase
{
    private double? _usdPrice;
    private double _chilizPriceUsdt;

    protected override string BuildRequestUri(int limit, Crypto crypto, List<Exchange> exchanges)
    {
        var chiliz = exchanges.SingleOrDefault(e => e.Name == "Chiliz");

        var endpoint = $"{chiliz.ApiUrl}/depth";
        var queryString = $"symbol={crypto.Symbol}CHZ&limit={limit}";

        return $"{endpoint}?{queryString}";
    }

    public override List<OrderBook> ParseOrderBookResponse(string content, Crypto crypto, Exchange exchange, double? usdPrice)
    {
        _usdPrice = usdPrice;
        
        _chilizPriceUsdt = GetChilizPrice(exchange).Result;
        
        var jObject = JObject.Parse(content);

        var orderBooks = new List<OrderBook>();
        int index = 0;
        var total = 0.0;
        foreach (var bid in jObject["bids"])
        {
            var price = ConvertPriceToBrl(double.Parse(bid[0].ToString(), CultureInfo.InvariantCulture) * _chilizPriceUsdt);

            var quantity = double.Parse(bid[1].ToString(), CultureInfo.InvariantCulture);

            total += price * quantity;

            orderBooks.Add(new OrderBook(Side.BUY, index, price, quantity, exchange.Id, crypto.Id, total));

            index++;
        }

        index = 0;
        total = 0.0;
        foreach (var ask in jObject["asks"])
        {
            var price = ConvertPriceToBrl(double.Parse(ask[0].ToString(), CultureInfo.InvariantCulture) * _chilizPriceUsdt);

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

    private async Task<double> GetChilizPrice(Exchange exchange)
    {
        var endpoint = $"{exchange.ApiUrl}/ticker/price?symbol=CHZUSDT";

        var requestUri = $"{endpoint}";

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        
        try
        {
            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var jObject = JObject.Parse(content);

                return Convert.ToDouble(jObject["price"]);
            }
        }
        catch
        {
            return 0.0;
        }
    }
}