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
    }

    public static async Task SeedTeamsAsync(RaceContext context)
    {
        // Idempotent: Only add teams if they don't exist (by unique Name)
        var seedTeams = new TeamSeed().Entities;
        foreach (var seedTeam in seedTeams)
        {
            if (!context.Teams.Any(t => t.Name == seedTeam.Name))
            {
                context.Teams.Add(new Team
                {
                    Name = seedTeam.Name,
                    ChampionShipPoints = seedTeam.ChampionShipPoints,
                    DateOfFoundation = seedTeam.DateOfFoundation,
                    OwnerName = seedTeam.OwnerName,
                    CreatedBy = seedTeam.CreatedBy,
                    CreatedAt = seedTeam.CreatedAt,
                    Active = seedTeam.Active
                });
            }
        }

        await context.SaveChangesAsync();
    }

    public static async Task SeedPilotsAsync(RaceContext context)
    {
        // Idempotent: Only add pilots if they don't exist (by unique Code)
        var seedPilots = new PilotSeed().Entities;

        var mercedesId = context.Teams.Single(t => t.Name == "Mercedes").Id;

        foreach (var seedPilot in seedPilots)
        {
            if (!context.Pilots.Any(p => p.Code == seedPilot.Code))
            {
                context.Pilots.Add(new Pilot
                {
                    Name = seedPilot.Name,
                    Number = seedPilot.Number,
                    Code = seedPilot.Code,
                    Nationality = seedPilot.Nationality,
                    TeamId = mercedesId,
                    CreatedBy = seedPilot.CreatedBy,
                    CreatedAt = seedPilot.CreatedAt,
                    Active = seedPilot.Active
                });
            }
        }

        await context.SaveChangesAsync();
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }    
}