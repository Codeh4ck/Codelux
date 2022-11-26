using System;
using System.Collections;
using System.Collections.Generic;

namespace Codelux.Paging
{
    public abstract class PagedListBase<T> : PageHeader, IPagedList<T>
    {
        protected readonly List<T> SubsetList;
        public T this[int index] => SubsetList[index];
        public int Count => SubsetList.Count;

        protected internal PagedListBase() {}

        protected internal PagedListBase(int pageNumber, int pageSize, int itemCount)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), pageNumber, "Value must be 1 or greater.");

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "Value must be 1 or greater.");

            SubsetList = new();

            ItemCount = itemCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            PageCount = ItemCount > 0 ? (int)Math.Ceiling(ItemCount / (double)PageSize) : 0;
            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < PageCount;
            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber >= PageCount;
            FirstItemIndex = ((PageNumber - 1) * PageSize + 1) - 1;
            
            int numberOfLastItemOnPage = FirstItemIndex + PageSize - 1;
            
            LastItemIndex = (numberOfLastItemOnPage > ItemCount
                ? ItemCount
                : numberOfLastItemOnPage) - 1;
        }

        public IPagedList GetPageHeader() => new PageHeader(this);

        public IEnumerator<T> GetEnumerator() => SubsetList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => SubsetList.GetEnumerator();
    }
}
