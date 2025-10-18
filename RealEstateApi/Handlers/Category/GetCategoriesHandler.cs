using RealEstate.Api.Handlers._Shared;
using RealEstate.Api.Handlers.Category.Dtos;
using RealEstate.Domain.Repositories;

namespace RealEstate.Api.Handlers.Category
{
    public class GetCategoriesHandler(IUnitOfWork unitOfWork) : QueryHandlerBase<GetCategoriesQuery, CategoriesDto>
    {
        private readonly ICategoryRepository _categoryRepository = unitOfWork.Categories;

        public override async Task<CategoriesDto> Handle(GetCategoriesQuery request, CancellationToken ct)
        {
            var categories = await _categoryRepository.GetAllAsync(ct);

            return CategoriesDto.Create(categories);
        }
    }

    public record GetCategoriesQuery(): IQuery<CategoriesDto>;
}
