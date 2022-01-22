using System.Collections.Generic;
using System.Linq;

namespace Codelux.Paging
{
    public class PagedList<T> : PagedListBase<T>
    {
        public PagedList(IEnumerable<T> superset, int pageNumber, int pageSize)
            : this(superset.AsQueryable<T>(), pageNumber, pageSize) { }

        public PagedList(IQueryable<T> superset, int pageNumber, int pageSize) : base(pageNumber, pageSize, superset?.Count() ?? 0)
        {
            if (superset != null && ItemCount > 0)
            {
                SubsetList.AddRange(
                    pageNumber == 1 
                        ? superset.Skip(0).Take(pageSize).ToList() 
                        : superset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
            }
        }
	}
}
