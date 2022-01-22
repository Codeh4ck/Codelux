using System.Collections.Generic;

namespace Codelux.Paging
{
	public class StaticPagedList<T> : PagedListBase<T>
	{
		public StaticPagedList(IEnumerable<T> subset, IPagedList metaData)
			: this(subset, metaData.PageNumber, metaData.PageSize, metaData.ItemCount)
		{
		}

		public StaticPagedList(IEnumerable<T> subset, int pageNumber, int pageSize, int itemCount)
			: base(pageNumber, pageSize, itemCount)
		{
			SubsetList.AddRange(subset);
		}
	}
}
