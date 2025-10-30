namespace RealEstate.Api.Handlers.Category.Dtos
{
    public class ExchangeRates
    {
        public string BaseCurrency { get; set; } = string.Empty;
        public IEnumerable<ExchangeRate> Rates { get; set; }

    }
}
