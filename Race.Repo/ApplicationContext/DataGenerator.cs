using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Race.Model.Models;
using System;
using System.Collections.Generic;

namespace Race.Repo.ApplicationContext
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider services)
        {
            using (var context = new RaceContext(services.GetRequiredService<DbContextOptions<RaceContext>>()))
            {
                if (context.Pilots.Any()) return;

                Result r;
                Pilot p = new Pilot
                {
                    PilotId = 1,
                    Code = "HAM",
                    Number = "44",
                    Name = "Hamilton",
                    Nationality = "British"
                };

                r = new Result(1, p.PilotId);

                p.Results = new List<Result>() { r };

                context.Pilots.AddRange(p);
                context.Results.AddRange(r);

                p = new Pilot()
                {
                    PilotId = 2,
                    Code = "HEI",
                    Number = "50",
                    Name = "Heidfeld",
                    Nationality = "German"
                };
                r = new Result(2, p.PilotId);
                p.Results = new List<Result>() { r };

                context.Pilots.AddRange(p);
                context.Results.AddRange(r);

                p = new Pilot()
                {
                    PilotId = 3,
                    Code = "ROS",
                    Number = "6",
                    Name = "Rosberg",
                    Nationality = "German"
                };
                r = new Result(3, p.PilotId);
                p.Results = new List<Result>() { r };

                context.Pilots.AddRange(p);
                context.Results.AddRange(r);

                p = new Pilot()
                {
                    PilotId = 4,
                    Code = "ALO",
                    Number = "14",
                    Name = "Alonso",
                    Nationality = "Spanish"
                };
                r = new Result(4, p.PilotId);
                p.Results = new List<Result>() { r };

                context.Pilots.AddRange(p);
                context.Results.AddRange(r);

                SavetoDatabase(context);
            }
        }

        private static void SavetoDatabase(RaceContext context)
        {
            context.Database.OpenConnection();

            try
            {
                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT dbo.Pilots ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT dbo.Pilots OFF");
            }
            finally { context.Database.CloseConnection(); }
        }
    }
}
