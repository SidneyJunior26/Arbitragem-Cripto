using Loader.Core.Services;
using Newtonsoft.Json.Linq;
using Solution.Core.Entities;
using Solution.Core.Enum;

namespace Loader.Infrastructure.Services;

public class BrasilBitcoinService : ExchangeServiceBase
{
    private string _apiUrl;
    
    public async Task<string> GetOrderBookByCoin(int limit, Crypto crypto, List<Exchange> exchanges)
    {
        try
        {
            var brasilBitcoin = exchanges.SingleOrDefault(e => e.Name == "Brasil Bitcoin");

            _apiUrl = brasilBitcoin.ApiUrl;

            var endpoint = $"/orderbook/{crypto.Symbol}";

            var requestUri = $"{_apiUrl}{endpoint}";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public override List<OrderBook> ParseOrderBookResponse(string content, Crypto crypto, Exchange exchange, double? usdPrice)
    {
        var jObject = JObject.Parse(content);
        
        if (jObject["success"] != null && Convert.ToBoolean(jObject["success"]) == false)
            return new List<OrderBook>();

        var orderBooks = new List<OrderBook>();
        int index = 0;
        var total = 0.0;
        foreach (var bid in jObject["buy"])
        {
            double price = double.Parse(bid["preco"].ToString());

            var quantity = double.Parse(bid["quantidade"].ToString());
            
            total += price * quantity;

            orderBooks.Add(new OrderBook(Side.BUY, index, price, quantity, exchange.Id, crypto.Id, total));

            if (index == 4)
                break;
            
            index++;
        }

        index = 0;
        total = 0.0;
        foreach (var ask in jObject["sell"])
        {
            double price = double.Parse(ask["preco"].ToString());

            var quantity = double.Parse(ask["quantidade"].ToString());
            
            total += price * quantity;

            orderBooks.Add(new OrderBook(Side.SELL, index, price, quantity, exchange.Id, crypto.Id, total));
            
            if (index == 4)
                break;

            index++;
        }
        
        return orderBooks;
    }

    protected override string BuildRequestUri(int limit, Crypto crypto, List<Exchange> exchanges)
    {
        var brasilBitcoin = exchanges.SingleOrDefault(e => e.Name == "Brasil Bitcoin");

        var endpoint = $"/orderbook/{crypto.Symbol}";

        return $"{brasilBitcoin.ApiUrl}{endpoint}";
    }
}