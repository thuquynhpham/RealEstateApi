using Microsoft.Identity.Abstractions;
using RealEstate.Core.Cache;
using System.Text.Json;

namespace RealEstate.Api.Infrastructure.Integrations.OpenApi
{
    public interface IExchangeRateService
    {
        Task<Dictionary<string, decimal>> GetExchangeRatesAsync(string currencySource, CancellationToken ct);
    }

    public class ExchangeRateService(IConfiguration config, HttpClient httpClient) : IExchangeRateService
    {
        private readonly IConfiguration _config = config;
        private readonly HttpClient _httpClient = httpClient;

        public async Task<Dictionary<string, decimal>> GetExchangeRatesAsync(string currencySource, CancellationToken ct)
        {
            //var exchangeRates = await _cacheProvider.GetAsync(CacheConfigs.ExchangeRateConfig, () => )
            var config = _config.GetSection("ExchangeRateApi");
            var baseUrl = config.GetValue<string>("BaseUrl");
            var accessKey = config.GetValue<string>("AccessKey");

            // Build the complete URL with query parameters
            var url = $"{baseUrl}?access_key={accessKey}&source={currencySource}";

            var response = await _httpClient.GetAsync(url, ct);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API call failed with status: {response.StatusCode}");
            }

            var jsonContent = await response.Content.ReadAsStringAsync(ct);
            
            // Debug: Log the actual JSON response to understand the structure
            Console.WriteLine($"API Response: {jsonContent}");

            try
            {
                var content = JsonSerializer.Deserialize<ExchangeRateResult>(jsonContent);
                
                // Return the rates from either Quotes or Rates property
                return content?.Quotes ?? content?.Rates ?? new Dictionary<string, decimal>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
                Console.WriteLine($"JSON Content: {jsonContent}");
                throw new InvalidOperationException($"Failed to deserialize exchange rate response: {ex.Message}", ex);
            }
        }


    }
}
