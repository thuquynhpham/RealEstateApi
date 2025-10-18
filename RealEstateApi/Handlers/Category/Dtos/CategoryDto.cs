using RealEstate.Api.Handlers._Shared;

namespace RealEstate.Api.Handlers.Category.Dtos
{
    public class CategoryDto: QueryApiResponse<CategoryDto>
    {
        public int CategoryId { get; init; }
        public string Name { get; init; }
        public string ImageUrl { get; init; }

        public static CategoryDto Create(Domain.Models.Category category)
        {
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                ImageUrl = category.ImageUrl,
            };
        }
    }
}
