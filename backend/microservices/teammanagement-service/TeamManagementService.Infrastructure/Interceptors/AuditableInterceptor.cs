using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using TeamManagementService.Domain.Interfaces;
using TeamManagementService.Infrastructure.ApplicationContext;

namespace TeamManagementService.Infrastructure.Interceptors;

// Interceptor to automatically set audit properties on entities implementing IAuditable
public sealed class AuditableInterceptor(ILogger<AuditableInterceptor> logger) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        logger.LogInformation("Saving changes in context: {ContextName}", eventData.Context?.GetType().Name);

        SetAuditProperties(eventData);
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken token)
    {
        logger.LogInformation("Saving changes asynchronously in context: {ContextName}", eventData.Context?.GetType().Name);

        SetAuditProperties(eventData);
        return await base.SavingChangesAsync(eventData, result, token);
    }

    private static void SetAuditProperties(DbContextEventData eventData)
    {
        if (eventData.Context is RaceContext context)
        {            
            var now = DateTimeOffset.UtcNow;
            var userName = eventData.Context.GetService<IHttpContextAccessor>()?.HttpContext?.User?.Identity?.Name;            

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is IAuditable auditable)
                {
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedAt = now;
                        auditable.CreatedBy = userName ?? "System";
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        auditable.UpdatedAt = now;
                        auditable.UpdatedBy = userName ?? "System";
                    }
                }
            }
        }
    }
}