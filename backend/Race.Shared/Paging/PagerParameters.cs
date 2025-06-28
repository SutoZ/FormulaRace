namespace Race.Shared.Paging;

public record PagerParameters(
    int PageIndex = 0,
    int PageSize = 10,
    string SortColumn = null,
    string SortOrder = null,
    string FilterColumn = null,
    string FilterQuery = null
);