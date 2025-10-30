using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Cache
{
    public class CacheConfig(string key, int expirationInMinutes)
    {
        public string Key { get; } = key;
        public int ExpirationInMinutes { get; } = expirationInMinutes;
    }
}
