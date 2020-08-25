using System;
using System.Collections.Generic;
using System.Text;

namespace Race.Repo.Dtos
{
    public class PagerDto
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
