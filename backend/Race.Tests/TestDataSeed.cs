using Microsoft.EntityFrameworkCore;
using Race.Model.Models;
using Race.Repo.ApplicationContext;
using System;
using System.Collections.Generic;

namespace Race.Tests
{
    public class TestDataSeed
    {
        public static void SeedData(RaceContext context)
        {
            PopulateTeamData(context);
            PopulatePilotData(context);
        }

        private static void PopulateTeamData(RaceContext context)
        {
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Teams ON");

            context.Teams.Add(new Team
            {
                Id = 1000,
                OwnerName = "Test Owner",
            //    Pilots = new List<Pilot> { new Pilot { Id = 1000 } },
                Name = "Test Team",
                ChampionShipPoints = 10,
                DateOfFoundation = DateTime.Now,
            });

            context.SaveChanges();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Teams OFF");
        }

        private static void PopulatePilotData(RaceContext context)
        {
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Pilots ON");

            context.Pilots.Add(new Pilot
            {
                Id = 1000,
                Code = "Test",
                Name = "Test Pilot",
                Nationality = "HUN",
                Number = "33",
                TeamId = 1000,
            });
            context.SaveChanges();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Pilots OFF");
        }
    }
}
