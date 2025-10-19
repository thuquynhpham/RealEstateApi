using RealEstate.Core;
using RealEstate.Domain.Models;
using RealEstate.Domain.Repositories;

namespace RealEstate.UnitTest.Fakes
{
    public class FakePropertyRepository(IList<Property> collection, IRequestContextProvider requestContextProvider) 
        : FakeRepository<Property>(collection, requestContextProvider), IPropertyRepository
    {
    }
}
