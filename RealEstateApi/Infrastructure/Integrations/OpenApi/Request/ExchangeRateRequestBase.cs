using RealEstate.Api.Infrastructure.Integrations.OpenApi.Responses;

namespace RealEstate.Api.Infrastructure.Integrations.OpenApi.Request
{
    public abstract class ExchangeRateRequestBase<T, TResult>() where T : IExchangeRateResponse
    {
        public abstract string EndPoint { get; }
        public abstract string CurrencySource { get; }

        public abstract TResult Serializer(ExchangeRateResult exchangeRateResult);
    }
}
