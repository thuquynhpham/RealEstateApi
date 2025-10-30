using RealEstate.Api.Handlers._Shared;

namespace RealEstate.Api.Handlers.Category.Dtos
{
    public class CategoriesDto: QueryApiResponse<CategoriesDto>
    {
        public IEnumerable<Domain.Models.Category> Categories { get; private set; } = [];

        public ExchangeRates? ExchangeRates { get; private set; } 

        public static CategoriesDto Create(IEnumerable<Domain.Models.Category> categories, string currencyBase, Dictionary<string, decimal> exchangeRates) {
            return new CategoriesDto
            {
                Categories = categories,
                ExchangeRates = Create(exchangeRates, currencyBase)
            };
        }

        public static ExchangeRates Create(Dictionary<string, decimal> exchangeRates, string currencyBase) {
            IList<ExchangeRate> rates = new List<ExchangeRate>();
            
            foreach (var rate in exchangeRates)
            {
                rates.Add(new ExchangeRate
                {
                    Currency = rate.Key.Substring(3),
                    Rate = rate.Value
                });
            }

            return new ExchangeRates
            {   BaseCurrency = currencyBase,
                Rates = rates
            };
        }
    }
}
