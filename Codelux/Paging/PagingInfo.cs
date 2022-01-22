using System.Collections.Generic;

namespace Codelux.Paging
{

    public interface IPagedList<T> : IPagedList, IEnumerable<T>
    {
        T this[int index] { get; }
        int Count { get; }
    }

    public interface IPagedList
    {
        public int PageNumber { get; }
        public int PageSize { get; }

        int PageCount { get; }
        int ItemCount { get; }
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
        bool IsFirstPage { get; }
        bool IsLastPage { get; }
        int FirstItemIndex { get; }
        int LastItemIndex { get; }
    }
}
