using System;
using System.Collections.Generic;
using System.Linq;

namespace Codelux.Paging
{
    public static class PagedListExtensions
    {
		public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> superset, int pageNumber, int pageSize)
		{
			return new PagedList<T>(superset, pageNumber, pageSize);
		}

		public static IPagedList<T> ToPagedList<T>(this IQueryable<T> superset, int pageNumber, int pageSize)
		{
			return new PagedList<T>(superset, pageNumber, pageSize);
		}

		public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> superset, int numberOfPages)
		{
			return superset
				.Select((item, index) => new { index, item })
				.GroupBy(x => x.index % numberOfPages)
				.Select(x => x.Select(y => y.item));
		}

		public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> superset, int pageSize)
        {
            T[] enumerable = superset as T[] ?? superset.ToArray();

            if (enumerable.Count() < pageSize)
				yield return enumerable;
			else
			{
				double numberOfPages = Math.Ceiling(enumerable.Count() / (double)pageSize);

				for (int i = 0; i < numberOfPages; i++)
					yield return enumerable.Skip(pageSize * i).Take(pageSize);
			}
        }
	}
}
