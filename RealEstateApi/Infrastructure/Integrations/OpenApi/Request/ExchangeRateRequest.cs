namespace RealEstate.Api.Infrastructure.Integrations.OpenApi.Request
{
    public class ExchangeRateRequest(string currencySource) : ExchangeRateRequestBase<ExchangeRate, Dictionary<string, decimal>>
    {
        private readonly string _currencySource = currencySource;

        public override string EndPoint => "";
        public override string CurrencySource => _currencySource;

        public override Dictionary<string, decimal> Serializer(ExchangeRateResult result)
        {
            return result.Quotes;
        }
    }
}
