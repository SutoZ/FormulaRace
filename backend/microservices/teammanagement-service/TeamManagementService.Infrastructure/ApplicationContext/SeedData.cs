using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeamManagementService.Domain.Models;
using TeamManagementService.Domain.Seed.Pilots;
using TeamManagementService.Domain.Seed.Teams;

namespace TeamManagementService.Infrastructure.ApplicationContext;

public static class SeedData
{
    public static async Task SeedAllAsync(RaceContext context, ILogger logger)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            logger.LogInformation("Seeding Teams at {Timestamp}", DateTimeOffset.UtcNow);
            await SeedTeamsAsync(context);

            logger.LogInformation("Seeding Pilots at {Timestamp}", DateTimeOffset.UtcNow);
            await SeedPilotsAsync(context);

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "An error occurred while seeding the database: {Message}", ex.Message);
            throw;
        }
    }

    private static async Task SeedTeamsAsync(RaceContext context)
    {
        if (await context.Teams.AnyAsync()) return; // Don't seed if data exists

        var seedTeams = new TeamSeed().Entities;
        foreach (var seedTeam in seedTeams)
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
        await context.SaveChangesAsync();
    }

    private static async Task SeedPilotsAsync(RaceContext context)
    {
        if (await context.Pilots.AnyAsync()) return; // Don't seed if data exists

        var seedPilots = new PilotSeed().Entities;
        var mercedesId = (await context.Teams.SingleOrDefaultAsync(t => t.Name == "Mercedes"))?.Id;

        if (mercedesId == null) return; // Cannot seed pilots without their team

        foreach (var seedPilot in seedPilots)
        {
            context.Pilots.Add(new Pilot
            {
                Name = seedPilot.Name,
                Number = seedPilot.Number,
                Code = seedPilot.Code,
                Nationality = seedPilot.Nationality,
                TeamId = mercedesId.Value,
                CreatedBy = seedPilot.CreatedBy,
                CreatedAt = seedPilot.CreatedAt,
                Active = seedPilot.Active
            });
        }
        await context.SaveChangesAsync();
    }
}