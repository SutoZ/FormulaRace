using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race.Shared.Extensions
{
    public static class EfExtensions
    {
        public static Task<List<TSource>> ToListAsync<TSource>(this IQueryable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!(source is IAsyncEnumerable<TSource>))
                return Task.FromResult(source.ToList());

            return source.ToListAsync();
        }
    }
}
