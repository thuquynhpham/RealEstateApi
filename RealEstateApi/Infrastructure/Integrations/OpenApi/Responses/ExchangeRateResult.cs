using System.Text.Json.Serialization;

namespace RealEstate.Api.Infrastructure.Integrations.OpenApi
{
    public class ExchangeRateResult
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("terms")]
        public string? Terms { get; set; }

        [JsonPropertyName("privacy")]
        public string? Privacy { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("source")]
        public string? Source { get; set; }

        [JsonPropertyName("quotes")]
        public Dictionary<string, decimal>? Quotes { get; set; }

        // Alternative structure for different API formats
        [JsonPropertyName("rates")]
        public Dictionary<string, decimal>? Rates { get; set; }

        [JsonPropertyName("base")]
        public string? Base { get; set; }

        [JsonPropertyName("date")]
        public string? Date { get; set; }
    }
}
