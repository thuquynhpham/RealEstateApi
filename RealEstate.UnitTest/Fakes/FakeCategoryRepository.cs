using RealEstate.Core;
using RealEstate.Domain.Models;
using RealEstate.Domain.Repositories;

namespace RealEstate.UnitTest.Fakes
{
    public class FakeCategoryRepository(IList<Category> collection, IRequestContextProvider requestContextProvider) 
        : FakeRepository<Category>(collection, requestContextProvider), ICategoryRepository
    {
    }
}
