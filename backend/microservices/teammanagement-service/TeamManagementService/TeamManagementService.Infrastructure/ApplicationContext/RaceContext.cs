using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeamManagementService.Domain.Models;
using TeamManagementService.Domain.Seed.Pilots;
using TeamManagementService.Domain.Seed.Teams;
using TeamManagementService.Infrastructure.EntityConfig;

namespace TeamManagementService.Infrastructure.ApplicationContext;

public class RaceContext(DbContextOptions<RaceContext> context) : IdentityDbContext<ApplicationUser>(context)
{
    public virtual DbSet<Pilot> Pilots { get; set; }
    public virtual DbSet<Team> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new PilotConfiguration());
        modelBuilder.ApplyConfiguration(new TeamConfiguration());

        SeedInitialDatas(modelBuilder);
    }

    private static void SeedInitialDatas(ModelBuilder modelBuilder)
    {
        //use reflection to get all seed classes in the assembly
        modelBuilder.Entity<Pilot>().HasData(new PilotSeed().Entities);
        modelBuilder.Entity<Team>().HasData(new TeamSeed().Entities);
    }
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }    
}