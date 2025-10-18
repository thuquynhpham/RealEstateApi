using RealEstate.Api.Handlers._Shared;

namespace RealEstate.Api.Handlers.Category.Dtos
{
    public class CategoriesDto: QueryApiResponse<CategoriesDto>
    {
        public IEnumerable<Domain.Models.Category> Categories { get; private set; } = [];

        public static CategoriesDto Create(IEnumerable<Domain.Models.Category> categories) {
            return new CategoriesDto
            {
                Categories = categories
            };
        }
    }
}
