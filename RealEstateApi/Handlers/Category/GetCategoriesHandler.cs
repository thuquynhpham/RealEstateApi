using RealEstate.Api.Handlers._Shared;
using RealEstate.Api.Handlers.Category.Dtos;
using RealEstate.Api.Infrastructure.Integrations.OpenApi;
using RealEstate.Domain.Repositories;

namespace RealEstate.Api.Handlers.Category
{
    public class GetCategoriesHandler(IUnitOfWork unitOfWork, IExchangeRateService exchangeRateService) : QueryHandlerBase<GetCategoriesQuery, CategoriesDto>
    {
        private readonly ICategoryRepository _categoryRepository = unitOfWork.Categories;
        private readonly IExchangeRateService _exchangeRateService = exchangeRateService;

        public override async Task<CategoriesDto> Handle(GetCategoriesQuery request, CancellationToken ct)
        {
            var categories = await _categoryRepository.GetAllAsync(ct);

            var currencyBase = "USD";
            var exchangeRates = await _exchangeRateService.GetExchangeRatesAsync(currencyBase, ct);

            return CategoriesDto.Create(categories, currencyBase, exchangeRates);
        }
    }

    public record GetCategoriesQuery(): IQuery<CategoriesDto>;
}
