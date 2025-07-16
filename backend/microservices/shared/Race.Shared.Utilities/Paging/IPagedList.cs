namespace Race.Shared.Utilities.Paging;

public interface IPagedList<T> where T : class
{
    IReadOnlyList<T> Data { get; set; }
    string FilterColumn { get; set; }
    string FilterQuery { get; set; }
    bool HasNextPage { get; }
    bool HasPreviousPage { get; }
    int PageIndex { get; set; }
    int PageSize { get; set; }
    string SortColumn { get; set; }
    string SortOrder { get; set; }
    int TotalCount { get; set; }
    int TotalPages { get; set; }
}
