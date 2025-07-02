using Race.Model.Models;
using System;

namespace Race.Model.Seed.Teams;

public class TeamSeed
{
    public Team[] Entities =
    [
        new Team
        {
            Id = 1,
            ChampionShipPoints = 20,
            Name = "Ferrari",
            DateOfFoundation= new DateTime(1950,04,04),
            OwnerName = "Mattia Binotto",
        },
        new Team
        {
            Id = 2,
            ChampionShipPoints = 30,
            Name = "Mercedes",
            DateOfFoundation= new DateTime(1970,04,04),
            OwnerName = "Toto Wolff"
        },
        new Team
        {
            Id = 3,
            Name = "McLaren",
            ChampionShipPoints = 80,
            OwnerName = "Andreas Seidl",
            DateOfFoundation = new DateTime(1960,05,04),
        },
        new Team
        {
            Id = 4,
            OwnerName = "Cyril Abiteboul",
            ChampionShipPoints = 20,
            Name = "Reanult",
            DateOfFoundation = new DateTime(1990,04,04),
        },
        new Team
        {
            Id = 5,
            Name = "Williams Racing",
            DateOfFoundation = new DateTime(1990,03,05),
            OwnerName = "Frank Williams",
            ChampionShipPoints = 5,
        },
        new Team
        {
            Id = 6,
            Name = "Alpha Tauri",
            DateOfFoundation = new DateTime(1990,09,05),
            OwnerName = "Franz Tost",
            ChampionShipPoints = 3
        }
    ];
};