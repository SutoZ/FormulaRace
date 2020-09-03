using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Dynamic.Core;     //can query throw column, which is not not at compile time, only at runtim by reflection
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Race.Shared.Paging
{
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// The data result (JSON array)
        /// </summary>
        public IList<T> Data { get; set; }
        /// <summary>
        /// Zero-based Index of Current page
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// Number of items contained in each page.
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Total items count
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// Total pages count
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// True if the current page has a previous page
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string FilterColumn { get; set; }
        public string FilterQuery { get; set; }

        private PagedList(IList<T> data,
            int count,
            int pageIndex,
            int pageSize,
            string sortColumn,
            string sortOrder,
            string filterColumn,
            string filterQuery)
        {
            Data = data;
            TotalCount = count;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            SortColumn = sortColumn;
            SortOrder = sortOrder;
            FilterColumn = filterColumn;
            FilterQuery = filterQuery;
        }

        /// <summary>
        /// True if the current page has a next page
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return (PageIndex + 1 <= TotalPages);
            }
        }

        public async static Task<PagedList<T>> CreateAsync(
            IQueryable<T> source,
            int pageIndex,
            int pageSize,
            string sortColumn = null,
            string sortOrder = null,
            string filterColumn = null,
            string filterQuery = null)
        {
            var count = source.Count();

            if (!string.IsNullOrEmpty(sortColumn) && IsValidProperty(sortColumn) && filterQuery != "null" && IsValidProperty(filterColumn))
            {
                sortOrder = !string.IsNullOrEmpty(sortOrder) && sortOrder.ToUpper() == "ASC" ? "ASC" : "DESC";
                source = source.Where($"{filterColumn}.Contains(@0)", filterQuery);
                source = source.OrderBy($"{sortColumn} {sortOrder}");
            }

            source = source.Skip(pageIndex * pageSize).Take(pageSize);
            var data = await source.ToListAsync();

            return new PagedList<T>(
                data,
                count,
                pageIndex,
                pageSize,
                sortColumn,
                sortOrder,
                filterColumn,
                filterQuery
                );
        }

        /// <summary>
        /// Check if the given property exists, to avoid SQL Injection Attacks!
        /// </summary>
        /// <param name="propertyName">The property column to check</param>
        /// <param name="throwExceptionIfNotFound"></param>
        /// <returns></returns>
        public static bool IsValidProperty(string propertyName, bool throwExceptionIfNotFound = true)
        {
            var prop = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (prop == null && throwExceptionIfNotFound)
            {
                throw new NotSupportedException($"Property --> {propertyName} <-- does not exist!");
            }
            return prop != null;
        }
    }
}
