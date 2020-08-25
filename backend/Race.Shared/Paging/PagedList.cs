using System;
using System.Collections.Generic;
using System.Linq;

namespace Race.Shared.Paging
{
    public class PagedList<T> : IPagedList<T>
    {
        private PagedList(IList<T> data, int count, int pageIndex, int pageSize)
        {

            Data = data;
            Count = count;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

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
        public int Count { get;  set; }
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

        /// <summary>
        /// True if the current page has a next page
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return (PageIndex + 1 < TotalPages);
            }
        }

        public static PagedList<T> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            source = source.Skip(pageIndex * pageSize).Take(pageSize);
            var data = source.ToList();

            return new PagedList<T>(data, count, pageIndex, pageSize);

        }
    }
}
