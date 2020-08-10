using Race.Model.Models;
using System;

namespace Race.Model.Seed.Pilots
{
    public class PilotSeed
    {
        public Pilot[] Entities => new[]
        {
                new Pilot
                {
                  PilotId = new Guid("595A50EC-75A6-434C-98F7-DC28A5AD6C00"),
                  Code = "HAM",
                  Number = "44",
                  Name = "Hamilton",
                  Nationality = "British"
                },
                new Pilot()
                {
                    PilotId = new Guid("C274F0F1-9ABA-41EF-B271-E671074D8AA2"),
                    Code = "HEI",
                    Number = "50",
                    Name = "Heidfeld",
                    Nationality = "German"
                },
                new Pilot()
                {
                    PilotId = new Guid("3DC612C0-5582-46BA-B2FA-6D72493E130D"),
                    Code = "ROS",
                    Number = "6",
                    Name = "Rosberg",
                    Nationality = "German"
                },
                new Pilot()
                {
                    PilotId = new Guid("76031F5A-3682-45AC-AFAC-8F31440CD863"),
                    Code = "ALO",
                    Number = "14",
                    Name = "Alonso",
                    Nationality = "Spanish"
                }
        };
    }
}
