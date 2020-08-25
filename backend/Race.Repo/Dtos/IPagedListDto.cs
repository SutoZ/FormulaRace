using System.Collections.Generic;

namespace Race.Repo.Dtos
{
    public interface IPagedListDto<T>
    {
        IList<T> Data { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
    }
}