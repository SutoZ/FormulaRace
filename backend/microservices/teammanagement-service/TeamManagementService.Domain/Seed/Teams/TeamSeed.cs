using TeamManagementService.Domain.Models;

namespace TeamManagementService.Domain.Seed.Teams;

public class TeamSeed
{
    public Team[] Entities =
    [
        new Team
        {
            ChampionShipPoints = 20,
            Name = "Ferrari",
            DateOfFoundation= new DateTime(1950,04,04),
            OwnerName = "Mattia Binotto",
            CreatedBy = "System",
            CreatedAt = DateTimeOffset.UtcNow,
            Active = true
        },
        new Team
        {
            ChampionShipPoints = 30,
            Name = "Mercedes",
            DateOfFoundation= new DateTime(1970,04,04),
            OwnerName = "Toto Wolff",
            CreatedBy = "System",
            CreatedAt = DateTimeOffset.UtcNow,
            Active = true
        },
        new Team
        {
            Name = "McLaren",
            ChampionShipPoints = 80,
            OwnerName = "Andreas Seidl",
            DateOfFoundation = new DateTime(1960,05,04),
            CreatedBy = "System",
            CreatedAt = DateTimeOffset.UtcNow,
            Active = true
        },
        new Team
        {
            OwnerName = "Cyril Abiteboul",
            ChampionShipPoints = 20,
            Name = "Reanult",
            DateOfFoundation = new DateTime(1990,04,04),
            CreatedBy = "System",
            CreatedAt = DateTimeOffset.UtcNow,
            Active = true
        },
        new Team
        {
            Name = "Williams Racing",
            DateOfFoundation = new DateTime(1990,03,05),
            OwnerName = "Frank Williams",
            ChampionShipPoints = 5,
            CreatedBy = "System",
            CreatedAt = DateTimeOffset.UtcNow,
            Active = true
        },
        new Team
        {
            Name = "Alpha Tauri",
            DateOfFoundation = new DateTime(1990,09,05),
            OwnerName = "Franz Tost",
            ChampionShipPoints = 3,
            CreatedBy = "System",
            CreatedAt = DateTimeOffset.UtcNow,
            Active = true
        }
    ];
};