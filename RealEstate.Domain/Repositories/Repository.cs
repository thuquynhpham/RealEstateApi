using Microsoft.EntityFrameworkCore;
using RealEstate.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity?> GetAsync(int id, CancellationToken ct);
        Task<List<TEntity>> GetAllAsync(CancellationToken ct, params Expression<Func<TEntity, object?>>[] includeProperties);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct, params Expression<Func<TEntity, object?>>[] includeProperties);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct, params Expression<Func<TEntity, object?>>[] includeProperties);
        ValueTask AddAsync(TEntity entity, CancellationToken ct);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        Task<int> RemoveAll(CancellationToken ct);

    }
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        protected readonly IRequestContextProvider RequestContextProvider;

        public Repository(DbContext context, IRequestContextProvider requestContextProvider)
        {
            Context = context;
            RequestContextProvider = requestContextProvider;
        }

        public async ValueTask<TEntity?> GetAsync(int id, CancellationToken ct)
        {
            return await Context.Set<TEntity>().FindAsync(id, ct);
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken ct, params Expression<Func<TEntity, object?>>[] includeProperties)
        {
            var context = GetQueryableContext();
            var query = context.AsQueryable();
            query = includeProperties.Aggregate(query, (q, w) => q.Include(w));

            return await query.ToListAsync(ct);
        }

        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct, params Expression<Func<TEntity, object?>>[] includeProperties)
        {
            var context = GetQueryableContext();
            var query = context.AsQueryable();
            query = includeProperties.Aggregate(query, (q, w) => q.Include(w));

            return await query.Where(predicate).FirstOrDefaultAsync(ct);
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct, params Expression<Func<TEntity, object?>>[] includeProperties)
        {
            var context = GetQueryableContext();
            var query = context.AsQueryable();
            query = includeProperties.Aggregate(query, (q, w) => q.Include(w));

            return await query.Where(predicate).ToListAsync(ct);
        }

        public async ValueTask AddAsync(TEntity entity, CancellationToken ct)
        {
            await Context.Set<TEntity>().AddAsync(entity, ct);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities, ct);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public Task<int> RemoveAll(CancellationToken ct)
        {
            return Context.Set<TEntity>().ExecuteDeleteAsync(ct);
        }

        protected virtual IQueryable<TEntity> GetQueryableContext()
        {
            return Context.Set<TEntity>().AsQueryable();
        }
    }
}
