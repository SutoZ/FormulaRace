using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TeamManagementService.Domain.Models;
using TeamManagementService.Infrastructure.EntityConfig;

namespace TeamManagementService.Infrastructure.ApplicationContext;

public class RaceContext : IdentityDbContext<ApplicationUser>
{
    public virtual DbSet<Pilot> Pilots { get; set; }
    public virtual DbSet<Team> Teams { get; set; }

    public RaceContext(DbContextOptions<RaceContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PilotConfiguration).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        // This configures EF Core to log the warning instead of throwing an exception.
        optionsBuilder.ConfigureWarnings(warnings =>
        {
            warnings.Log(RelationalEventId.PendingModelChangesWarning);
        });
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}