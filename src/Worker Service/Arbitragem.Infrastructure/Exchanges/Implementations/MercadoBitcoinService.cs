using Arbitragem.Infrastructure.Exchanges.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Arbitragem.Infrastructure.Exchanges.Implementations;

public class MercadoBitcoinService : IMercadoBitcoinService
{
    private readonly string _apiUrl;

    public MercadoBitcoinService(IConfiguration config)
    {
        _apiUrl = config["ApiUrl:MercadoBitcoin"];
    }

    public async Task<string> GetOrderBookByCoin(string symbol, int limit)
    {
        var endpoint = $"/{symbol}-BRL/orderbook";
        var queryString = $"limit={limit}";

        var requestUri = $"{_apiUrl}{endpoint}?{queryString}";

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        try
        {
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
}

