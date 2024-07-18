using Arbitragem.Infrastructure.Exchanges.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Arbitragem.Infrastructure.Exchanges.Implementations;

public class CoinGeckoService : ICoinGeckoService
{
    private readonly string _apiUrl;
    private readonly string _apiToken;

    public CoinGeckoService(IConfiguration config)
    {
        _apiUrl = config["ApiUrl:CoinGecko"];
        _apiToken = config["APITokens:CoinGecko"];
    }

    public async Task<HttpResponseMessage> GetCoins()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, _apiUrl);
        request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiToken);

        try
        {
            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                return response;
            }
        }
        catch
        {
            return null;
        }
    }
}

