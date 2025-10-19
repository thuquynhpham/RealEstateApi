using RealEstate.Core;
using RealEstate.Domain.Models;
using RealEstate.Domain.Repositories;

namespace RealEstate.UnitTest.Fakes
{
    public class FakeUserRepository(IList<User> collection, IRequestContextProvider requestContextProvider) 
        : FakeRepository<User>(collection, requestContextProvider), IUserRepository
    {
    }
}
