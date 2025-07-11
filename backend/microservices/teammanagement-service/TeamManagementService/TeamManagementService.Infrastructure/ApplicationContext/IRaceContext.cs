using Microsoft.EntityFrameworkCore;
using TeamManagementService.Domain.Models;

namespace TeamManagementService.Infrastructure.ApplicationContext;

public interface IRaceContext
{
    DbSet<Pilot> Pilots { get; set; }
    DbSet<Team> Teams { get; set; }
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
}