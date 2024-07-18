using Arbitragem.Infrastructure.Exchanges.Interfaces;
using Microsoft.Extensions.Configuration;
using ArbitraX.Infrastructure.Utils;

namespace Arbitragem.Infrastructure.Exchanges.Implementations;

public class BinanceService : IBinanceService
{
    private readonly string _apiUrl;
    private readonly string _apiKey;
    private readonly string _secretKey;

    public BinanceService(IConfiguration config)
    {
        _apiUrl = config["ApiUrl:Binance"];
        _apiKey = config["APIKeys:Binance"];
        _secretKey = config["APISecretKeys:Binance"];
    }

    public async Task<string> GetOrderBookByCoin(string symbol, int limit)
    {
        var endpoint = $"{_apiUrl}/depth";
        var timestamp = Utilities.GenerateTimeStamp();
        var queryString = $"symbol={symbol}USDT&limit={limit}";

        //var signature = Utilities.CreateSignature(queryString, _secretKey);
        var requestUri = $"{endpoint}?{queryString}";

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Add("X-MBX-APIKEY", _apiKey);
        
        try
        {
            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request).ConfigureAwait(false);
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

