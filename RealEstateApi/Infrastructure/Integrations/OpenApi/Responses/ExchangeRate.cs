using RealEstate.Api.Infrastructure.Integrations.OpenApi.Responses;

namespace RealEstate.Api.Infrastructure.Integrations.OpenApi
{
    public class ExchangeRate : IExchangeRateResponse
    {
        public bool Success { get; set; }
        public required string Terms { get; set; }
        public required string Privacy { get; set; }
        public long Timestamp { get; set; }
        public required string Source { get; set; }
        public required Dictionary<string, decimal> Quotes { get; set; }
    }
}
