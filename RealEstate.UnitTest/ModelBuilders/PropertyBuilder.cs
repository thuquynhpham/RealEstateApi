using RealEstate.Domain.Models;

namespace RealEstate.UnitTest.ModelBuilders
{
    public class PropertyBuilder : ModelBuilderBase
    {
        private Property _property;

        public PropertyBuilder(int id, string name, int categoryId, int userId, bool isTrending)
        {
            _property = new Property
            {
                PropertyId = id,
                Name = name,
                CategoryId = categoryId,
                UserId = userId,
                IsTrending = isTrending,
                Detail = "Sample detail",
                Address = "Sample address",
                Price = 100000,
            };
        }

        public Property Build()
        {
            return _property;
        }
    }
}
