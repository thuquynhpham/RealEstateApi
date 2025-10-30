namespace RealEstate.Core.Cache
{
    public interface ICacheProvider
    {
        Task<T?> GetAsync<T>(CacheConfig cacheConfig, Func<Task<T>> func) where T : class;
        T? Get<T>(CacheConfig cacheConfig);
        void Set<T>(string key, T value);
        void Clear(IEnumerable<string> keys);
    }
}
