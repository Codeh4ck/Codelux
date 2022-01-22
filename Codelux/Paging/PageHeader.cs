namespace Codelux.Paging
{
    public class PageHeader : IPagedList
    {
        protected PageHeader() { }

        public PageHeader(IPagedList pagedList)
        {
            PageNumber = pagedList.PageNumber;
            PageSize = pagedList.PageSize;
            PageCount = pagedList.PageCount;
            ItemCount = pagedList.ItemCount;
            HasNextPage = pagedList.HasNextPage;
            HasPreviousPage = pagedList.HasPreviousPage;
            IsFirstPage = pagedList.IsFirstPage;
            IsLastPage = pagedList.IsLastPage;
            FirstItemIndex = pagedList.FirstItemIndex;
            LastItemIndex = pagedList.LastItemIndex;
        }


        public int PageNumber { get; protected set; }
        public int PageSize { get; protected set; }
        public int PageCount { get; protected set; }
        public int ItemCount { get; protected set; }
        public bool HasNextPage { get; protected set; }
        public bool HasPreviousPage { get; protected set; }
        public bool IsFirstPage { get; protected set; }
        public bool IsLastPage { get; protected set; }
        public int FirstItemIndex { get; protected set; }
        public int LastItemIndex { get; protected set; }
    }
}
