using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using TeamManagementService.Domain.Interfaces;
using TeamManagementService.Infrastructure.ApplicationContext;

namespace TeamManagementService.Infrastructure.Interceptors;

public sealed class SoftDeleteInterceptor(ILogger<SoftDeleteInterceptor> logger) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken token)
    {
        logger.LogInformation("Soft deleting entities in context: {ContextName}", eventData.Context?.GetType().Name);

        if (eventData.Context is RaceContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDelete softDelete)
                {
                    entry.State = EntityState.Modified;
                    softDelete.Active = false;
                    softDelete.DeletedAt = DateTime.UtcNow;
                }
            }
        }
        return await base.SavingChangesAsync(eventData, result, token);
    }
}