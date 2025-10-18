using RealEstate.Domain.Models;

namespace RealEstate.Domain.DBI.SeedData
{
    internal class CategoryDataInitialiser : DataInitialiser<Category>
    {
        protected override IList<Category> GetData()
        {
            return
            [
                new() { CategoryId = 1, Name = "Apartment", ImageUrl = "apartments.png"},
                new() { CategoryId = 2, Name = "House", ImageUrl = "houses.png"},
                new() { CategoryId = 3, Name = "Supermarket", ImageUrl = "supermarket.png"}
            ];
        }
    }
}
