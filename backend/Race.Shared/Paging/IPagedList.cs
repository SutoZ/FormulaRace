using System.Collections.Generic;

namespace Race.Shared.Paging
{
    public interface IPagedList<T>
    {
        int TotalCount { get; }
        IList<T> Data { get; set; }
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int TotalPages { get; set; }
    }
}