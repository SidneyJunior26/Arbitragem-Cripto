using System.Globalization;
using ArbitraX.Infrastructure.Services.Interfaces;
using ArbitraX.Infrastructure.Services.Response;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ArbitraX.Infrastructure.Services;

public class AwesomeService : IAwesomeService
{
    private readonly string _apiUrl;

    public AwesomeService(IConfiguration config)
    {
        _apiUrl = config["ApiUrl:Awesome"];
    }

    public async Task<double> GetUsdPrice()
    {
        var requestUri = $"{_apiUrl}/last/USD-BRL";

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        try
        {
            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                // Desserializar o JSON
                var usdPriceResponse = JsonConvert.DeserializeObject<UsdPriceResponse>(content);

                if (usdPriceResponse != null && usdPriceResponse.USDBRL != null)
                {
                    // Converter o valor de "bid" para double
                    if (double.TryParse(usdPriceResponse.USDBRL.bid, CultureInfo.InvariantCulture, out double bidValue))
                    {
                        return bidValue;
                    }
                }

                return 0; // Valor padrão caso não seja possível obter o "bid"
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter o preço do USD: {ex.Message}");
            return 0;
        }
    }
}

