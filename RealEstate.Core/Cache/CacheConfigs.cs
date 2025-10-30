namespace RealEstate.Core.Cache
{
    public class CacheConfigs
    {
        public static CacheConfig CategoriesCacheConfig = new CacheConfig("categories_cache_key", 60 * 24); // 24 hours

        public static CacheConfig ExchangeRateConfig = new CacheConfig("exchange_rate_cache_key", 60* 26);
    }
}
