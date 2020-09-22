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
            PopulatePilotData(context);
            PopulateTeamData(context);
        }

        private static void PopulateTeamData(RaceContext context)
        {
            context.Teams.Add(new Team
            {
                Id = 1000,
                OwnerName = "Test Owner",
                Pilots = new List<Pilot> { new Pilot { Id = 1000 } },
                Name = "Test Team",
                ChampionShipPoints = 10,
                DateOfFoundation = DateTime.Now,
            });

            context.SaveChanges();
        }

        private static void PopulatePilotData(RaceContext context)
        {
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
        }
    }
}
