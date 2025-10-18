using RealEstate.Core;
using RealEstate.Domain.DBI;
using RealEstate.Domain.Models;

namespace RealEstate.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {

    }
    public class CategoryRepository(RealEstateDbContext dbContext, IRequestContextProvider requestContextProvider) 
        : Repository<Category>(dbContext, requestContextProvider), ICategoryRepository
    {
    }
}
