using Loader.Core.Interfaces;
using Solution.Core.Entities;

namespace Loader.Core.Services;

public abstract class ExchangeServiceBase : IExchangeService
{
    protected abstract string BuildRequestUri(int limit, Crypto crypto, List<Exchange> exchanges);

    public abstract List<OrderBook> ParseOrderBookResponse(string content, Crypto crypto, Exchange exchange, double? usdPrice);

    public async Task<string> GetOrderBookByCoin(int limit, Crypto crypto, List<Exchange> exchanges)
    {
        var requestUri = BuildRequestUri(limit, crypto, exchanges);

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

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
        catch
        {
            return "";
        }
    }

    protected virtual double ConvertPriceToBrl(double value)
    {
        return value * 1;
    }
}

