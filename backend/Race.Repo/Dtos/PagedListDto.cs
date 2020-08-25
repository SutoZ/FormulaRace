using System;
using System.Collections.Generic;
using System.Text;

namespace Race.Repo.Dtos
{
    public class PagedListDto<T> : IPagedListDto<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IList<T> Data { get; set; }
    }
}
