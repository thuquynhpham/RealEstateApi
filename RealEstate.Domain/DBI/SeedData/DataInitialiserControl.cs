using System.Collections.Generic;

namespace RealEstate.Domain.DBI.SeedData
{
    public abstract class DataInitialiserControl
    {
        public static bool SkipInitData { get; set; }
    }

    internal abstract class DataInitialiser<T>: DataInitialiserControl
    {
        public IList<T> Data => SkipInitData ? new List<T>() : GetData();

        protected abstract IList<T> GetData();
    }
}
