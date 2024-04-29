using System.Collections.Generic;
using System.Linq;

namespace X.PagedList
{
    public class PagedObject<T>
    {
        public PagedObject(IQueryable<T> superset, int pageNumber, int pageSize)
        {
            var totalItemCount = superset.Count();

            if (totalItemCount > 0 && superset != null)
            {
                var skip = (pageNumber - 1) * pageSize;

                Items = superset.Skip(skip).Take(pageSize);
            }

            MetaData = new StaticPagedList<T>(Items, pageNumber, pageSize, totalItemCount);
        }

        public PagedObject(IEnumerable<T> superset, int pageNumber, int pageSize)
            : this(superset.AsQueryable(), pageNumber, pageSize)
        { }

        public PagedListMetaData MetaData { get; private set; }

        public IEnumerable<T> Items { get; private set; } = new List<T>();
    }
}