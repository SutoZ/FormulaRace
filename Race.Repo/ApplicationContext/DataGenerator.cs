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
                    PilotId = new Guid("595A50EC-75A6-434C-98F7-DC28A5AD6C00"),
                    Code = "HAM",
                    Number = "44",
                    Name = "Hamilton",
                    Nationality = "British"
                };

                r = new Result(resultId: new Guid("E173913C-A132-484B-8AD4-8A4A2F24B693"), p.PilotId);

                p.Results = new List<Result>() { r };

                context.Pilots.AddRange(p);
                context.Results.AddRange(r);

                p = new Pilot()
                {
                    PilotId = new Guid("C274F0F1-9ABA-41EF-B271-E671074D8AA2"),
                    Code = "HEI",
                    Number = "50",
                    Name = "Heidfeld",
                    Nationality = "German"
                };
                r = new Result(resultId: new Guid("72655CEC-CEB4-49AB-9056-790D24C01355"), p.PilotId);
                p.Results = new List<Result>() { r };

                context.Pilots.AddRange(p);
                context.Results.AddRange(r);

                p = new Pilot()
                {
                    PilotId = new Guid("3DC612C0-5582-46BA-B2FA-6D72493E130D"),
                    Code = "ROS",
                    Number = "6",
                    Name = "Rosberg",
                    Nationality = "German"
                };
                r = new Result(resultId: new Guid("95397422-F23E-4754-B2E6-266CD41F9C4C"), p.PilotId);
                p.Results = new List<Result>() { r };

                context.Pilots.AddRange(p);
                context.Results.AddRange(r);

                p = new Pilot()
                {
                    PilotId = new Guid("76031F5A-3682-45AC-AFAC-8F31440CD863"),
                    Code = "ALO",
                    Number = "14",
                    Name = "Alonso",
                    Nationality = "Spanish"
                };
                r = new Result(resultId: new Guid("B9B0693F-31FA-49E4-8747-5EE217046BDB"), p.PilotId);
                p.Results = new List<Result>() { r };

                context.Pilots.AddRange(p);
                context.Results.AddRange(r);
                context.SaveChanges();

                //SavetoDatabase(context);
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
