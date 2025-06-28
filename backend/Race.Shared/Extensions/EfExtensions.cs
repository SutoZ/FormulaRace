using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Shared.Extensions;

public static class EfExtensions
{
    public static Task<List<TSource>> ToListSafeAsync<TSource>(this IQueryable<TSource> source, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (source.Provider is IAsyncQueryProvider)
            return EntityFrameworkQueryableExtensions.ToListAsync(source, token);

        return source.ToListSafeAsync(token);
    }
}