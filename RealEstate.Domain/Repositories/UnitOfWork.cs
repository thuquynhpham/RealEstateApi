using Microsoft.EntityFrameworkCore;
using RealEstate.Core;
using RealEstate.Domain.DBI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Domain.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> CompleteAsync(CancellationToken ct);
        ICategoryRepository Categories { get; }
        IUserRepository Users { get; }
        IPropertyRepository Properties { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly RealEstateDbContext _dbContext;
        private readonly IRequestContextProvider _requestContextProvider;

        private bool _disposed = false;
        private ICategoryRepository? _categories;
        private IUserRepository? _users;
        private IPropertyRepository? _properties;

        public UnitOfWork(RealEstateDbContext dbContext, IRequestContextProvider requestContextProvider)
        {
            _dbContext = dbContext;
            _requestContextProvider = requestContextProvider;
        }

        public ICategoryRepository Categories => _categories ??= new CategoryRepository(_dbContext, _requestContextProvider);
        public IUserRepository Users => _users ??= new UserRepository(_dbContext, _requestContextProvider);
        public IPropertyRepository Properties => _properties ??= new PropertyRepository(_dbContext, _requestContextProvider);

        public async Task<int> CompleteAsync(CancellationToken ct)
        {
            var changedEntities = _dbContext.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged && x.State != EntityState.Deleted);
            foreach (var changedEntity in changedEntities)
            {
                if(changedEntity.Entity is IValidatableObject entity)
                {
                    var validationResults = entity.Validate(new ValidationContext(entity));

                    if (validationResults.Any())
                    {
                        var exceptions = new List<ValidationException>();
                        foreach (var validationResult in validationResults)
                        {
                            exceptions.Add(new ValidationException(validationResult.ErrorMessage, null, validationResult.MemberNames.First()));
                        }

                        throw new AggregateException("Validation Errors", exceptions);
                    }
                }
            }

            return await _dbContext.SaveChangesAsync(ct);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            _disposed = true;
        }

    }
}
