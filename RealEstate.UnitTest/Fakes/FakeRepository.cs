using RealEstate.Core;
using RealEstate.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.UnitTest.Fakes
{
    public class FakeRepository<T> : IRepository<T> where T : class
    {
        protected readonly IList<T> _collection;
        private IRequestContextProvider _requestContextProvider;

        protected FakeRepository(IList<T> collection, IRequestContextProvider requestContextProvider)
        {
            _collection = collection;
            _requestContextProvider = requestContextProvider;
        }

        public ValueTask AddAsync(T entity, CancellationToken ct = default)
        {
            _collection.Add(entity);
            return ValueTask.CompletedTask;
        }

        public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
        {
            foreach (var entity in entities)
            {
                _collection.Add(entity);
            }

            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default, params Expression<Func<T, object?>>[] includeProperties)
        {
            var queryResult = _collection.AsQueryable().Where(predicate).ToList();
            FilterByProperties(includeProperties, queryResult);

            return Task.FromResult(queryResult.AsEnumerable());
        }

        public Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default, params Expression<Func<T, object?>>[] includeProperties)
        {
            var queryResult = _collection.AsQueryable().Where(predicate).ToList();
            FilterByProperties(includeProperties, queryResult);
            return Task.FromResult(queryResult.FirstOrDefault());
        }

        public ValueTask<T?> GetAsync(int id, CancellationToken ct = default)
        {
            return ValueTask.FromResult(_collection.FirstOrDefault(x =>
            {
                var key = x.GetType().GetProperties().FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))) 
                ?? throw new Exception("No key attribute found on entity");

                var keyValue = key.GetValue(x);
                return keyValue != null && (int)keyValue == id;
            }
            ));
        }

        public Task<List<T>> GetAllAsync(CancellationToken ct = default, params Expression<Func<T, object?>>[] includeProperties)
        {
            var queryResult = _collection.AsQueryable().ToList();
            FilterByProperties(includeProperties, queryResult);
            return Task.FromResult(queryResult.ToList());
        }

        public void Remove(T entity)
        {
            _collection.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _collection.Remove(entity);
            }
        }

        public void Update(T entity)
        {
            // In a fake repository, the entity is already in the collection, so no action is needed.
        }

        public Task<int> RemoveAll(CancellationToken ct)
        {
            _collection.Clear();
            return Task.FromResult(0);
        }

        protected static void FilterByProperties(Expression<Func<T, object?>>[] includeProperties, IEnumerable<T> query)
        {
            foreach (var entity in query)
            {
                foreach (var prop in entity.GetType().GetProperties())
                {
                    if(prop.PropertyType.GetTypeInfo().IsClass && prop.PropertyType.Assembly.FullName == entity.GetType().Assembly.FullName)
                    {
                        var included = includeProperties.Select(x => ((MemberExpression)x.Body).Member.Name);
                        if (!included.Contains(prop.PropertyType.Name))
                        {
                            prop.SetValue(entity, null);
                        }
                    }
                }
            }
        }
    }
}
