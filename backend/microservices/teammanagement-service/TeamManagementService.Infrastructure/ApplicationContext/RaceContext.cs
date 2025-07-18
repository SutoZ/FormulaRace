using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TeamManagementService.Domain.Models;
using TeamManagementService.Domain.Seed.Pilots;
using TeamManagementService.Domain.Seed.Teams;
using TeamManagementService.Infrastructure.EntityConfig;

namespace TeamManagementService.Infrastructure.ApplicationContext;

public class RaceContext : IdentityDbContext<ApplicationUser>
{
    private readonly ILogger<RaceContext>? logger;

    public virtual DbSet<Pilot> Pilots { get; set; }
    public virtual DbSet<Team> Teams { get; set; }

    public RaceContext(DbContextOptions<RaceContext> options, ILogger<RaceContext>? logger = null) : base(options)
    {
        this.logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var softDeleteInterface = typeof(Domain.Interfaces.ISoftDelete);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;
            if (softDeleteInterface.IsAssignableFrom(clrType))
            {
                // Build the lambda: e => e.Active
                var parameter = Expression.Parameter(clrType, "e");
                var property = Expression.Property(parameter, "Active");
                var lambda = Expression.Lambda(property, parameter);

                entityType.SetQueryFilter(lambda);
            }
        }

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

    public async Task SeedAllAsync()
    {
        await using var transaction = await Database.BeginTransactionAsync();
        try
        {
            logger?.LogInformation("Seeding Teams at {Timestamp}", DateTimeOffset.UtcNow);
            await SeedTeamsAsync(this);

            logger?.LogInformation("Seeding Pilots at {Timestamp}", DateTimeOffset.UtcNow);
            await SeedPilotsAsync(this);

            await transaction.CommitAsync();
        }
        catch (DbUpdateException ex)
        {
            await transaction.RollbackAsync();
            logger?.LogError(ex, "An error occurred while seeding the database: {Message}", ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger?.LogError(ex, "An error occurred while seeding the database: {Message}", ex.Message);
            throw;
        }
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}