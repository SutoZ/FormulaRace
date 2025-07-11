using System.Reflection;
using System.Linq.Dynamic.Core;     //can query throw column, which is not at compile time, only at runtime by reflection
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Race.Shared.Utilities.Extensions;

namespace Race.Shared.Utilities.Paging;

public class PagedList<T> : IPagedList<T> where T : class
{
    public IReadOnlyList<T> Data { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage => PageIndex > 0;
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
    public string FilterColumn { get; set; }
    public string FilterQuery { get; set; }

    private PagedList(IReadOnlyList<T> data, int count, PagerParameters pagerParameters)
    {
        Data = data;
        TotalCount = count;
        PageIndex = pagerParameters.PageIndex;
        PageSize = pagerParameters.PageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pagerParameters.PageSize);
        SortColumn = pagerParameters.SortColumn;
        SortOrder = pagerParameters.SortOrder;
        FilterColumn = pagerParameters.FilterColumn;
        FilterQuery = pagerParameters.FilterQuery;
    }

    /// <summary>
    /// True if the current page has a next page
    /// </summary>
    public bool HasNextPage => PageIndex + 1 <= TotalPages;

    public async static Task<PagedList<TDto>> CreateAsync<TEntity, TDto>(
        IQueryable<TEntity> source,
        PagerParameters pagerParameters,
        Expression<Func<TEntity, TDto>> selector,
        CancellationToken token)
        where TEntity : class
        where TDto : class
    {
        ArgumentNullException.ThrowIfNull(pagerParameters, nameof(pagerParameters));

        var count = await source.CountAsync(token);

        var sortColumn = pagerParameters.SortColumn;
        var sortOrder = !string.IsNullOrEmpty(pagerParameters.SortOrder) && pagerParameters.SortOrder.Equals("ASC", StringComparison.CurrentCultureIgnoreCase) ? "ASC" : "DESC";
        var filterColumn = pagerParameters.FilterColumn;
        var filterQuery = pagerParameters.FilterQuery;
        var pageIndex = pagerParameters.PageIndex;
        var pageSize = pagerParameters.PageSize;


        //Filtering before sorting and paging for efficiency
        if (!string.IsNullOrWhiteSpace(filterColumn) &&
            !string.IsNullOrWhiteSpace(filterQuery)
            && IsValidProperty(filterColumn))
            source = source.Where($"{filterColumn}.Contains(@0)", filterQuery);

        //Sorting
        if (!string.IsNullOrEmpty(pagerParameters.SortColumn) && IsValidProperty(pagerParameters.SortColumn))
            source = source.OrderBy($"{sortColumn} {sortOrder}", token);

        //Paging
        if (pageSize != 0)
            source = source.Skip(pageIndex * pageSize).Take(pageSize);

        //Projection to DTO
        var result = await source.Select(selector).ToListSafeAsync(token);

        return new PagedList<TDto>(result, count, pagerParameters);
    }

    /// <summary>
    /// Check if the given property exists, to avoid SQL Injection Attacks!
    /// </summary>
    /// <param name="propertyName">The property column to check</param>
    /// <param name="throwExceptionIfNotFound"></param>
    /// <returns></returns>
    public static bool IsValidProperty(string propertyName, bool throwExceptionIfNotFound = true)
    {
        var prop = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

        if (prop is null && throwExceptionIfNotFound)
            throw new NotSupportedException($"Property --> {propertyName} <-- does not exist!");

        return prop is not null;
    }
}