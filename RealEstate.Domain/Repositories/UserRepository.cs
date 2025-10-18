using RealEstate.Core;
using RealEstate.Domain.DBI;
using RealEstate.Domain.Models;

namespace RealEstate.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

    }
    public class UserRepository(RealEstateDbContext dbContext, IRequestContextProvider requestContextProvider) 
        : Repository<User>(dbContext, requestContextProvider), IUserRepository
    {
    }
}
