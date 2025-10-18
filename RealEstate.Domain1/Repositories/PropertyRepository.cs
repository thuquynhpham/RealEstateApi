using RealEstate.Core;
using RealEstate.Domain.DBI;
using RealEstate.Domain.Models;

namespace RealEstate.Domain.Repositories
{
    public interface IPropertyRepository : IRepository<Property>
    {

    }
    public class PropertyRepository(RealEstateDbContext dbContext, IRequestContextProvider requestContextProvider) 
        : Repository<Property>(dbContext, requestContextProvider), IPropertyRepository
    {
    }
}
