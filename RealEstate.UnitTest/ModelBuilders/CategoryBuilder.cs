using RealEstate.Domain.Models;

namespace RealEstate.UnitTest.ModelBuilders
{
    public class CategoryBuilder : ModelBuilderBase
    {
        private Category _category;

        public CategoryBuilder(int id, string name)
        {
            _category = new Category
            {
                CategoryId = id,
                Name = name,
                ImageUrl = "https://example.com/category.jpg"
            };
        }

        public Category Build()
        {
            return _category;
        }
    }
}
