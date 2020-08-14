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
                  Id = 1,
                  Code = "HAM",
                  Number = "44",
                  Name = "Lewis Hamilton",
                  TeamId = 1,
                  Nationality = "British"
                },
                new Pilot()
                {
                    Id = 2,
                    Code = "HEI",
                    Number = "50",
                    Name = "Nick Heidfeld",
                  TeamId = 1,

                    Nationality = "German"
                },
                new Pilot()
                {
                    Id = 3,
                    Code = "ROS",
                  TeamId = 1,
                    Number = "6",
                    Name = "Nico Rosberg",
                    Nationality = "German"
                },
                new Pilot()
                {
                    Id = 4,
                    Code = "RAI",
                  TeamId = 1,
                    Number = "14",
                    Name = "Kimi Raikonnen",
                    Nationality = "Finnish"
                },
                new Pilot()
                {
                    Id = 5,
                    Code = "KUB",
                    Number = "19",
                    Name = "Robert Kubica",
                  TeamId = 1,
                    Nationality = "Polish"
                },
            new Pilot()
                {
                    Id = 6,
                    Code = "GLO",
                  TeamId = 2,
                    Number = "20",
                    Name = "Timo Glock",
                    Nationality = "German"
                },
            new Pilot()
                {
                    Id = 7,
                    Code = "SAT",
                    Number = "21",
                  TeamId = 2,
                    Name = "Tacuma Sato",
                    Nationality = "Japanese"
                },
        /*    new Pilot()
                {
                    Id = new Guid("AEB5A563-9B9F-44DB-A56E-D7404ADE9BE3"),
                    Code = "MAS",
                    Number = "22",
                  TeamId = 1,
                    Name = "Felipe Massa",
                    Nationality = "Brazilian"
                },
            new Pilot()
                {
                    Id = new Guid("918AD535-B482-40AF-ACB5-0219418B51FE"),
                    Code = "COU",
                    Number = "23",
                  TeamId = 1,
                    Name = "David Coulthard",
                    Nationality = "British"
                },
            new Pilot()
                {
                    Id = new Guid("3F179E30-F470-4253-A90C-FDC5CEF4F76E"),
                    Code = "TRU",
                  TeamId = 1,
                    Number = "24",
                    Name = "Jarno Trulli",
                    Nationality = "Italian"
                },
            new Pilot()
                {
                    Id = new Guid("8E1B2BE1-4A7C-4EC2-9072-A0AE97642605"),
                    Code = "SUT",
                  TeamId = 1,
                    Number = "25",
                    Name = "Adrian Sutil",
                    Nationality = "German"
                },
            new Pilot()
                {
                    Id = new Guid("30E3C76B-106A-42AF-880A-0E7A4B9D18E8"),
                    Code = "WEB",
                    Number = "26",
                  TeamId = 1,
                    Name = "Mark Webber",
                    Nationality = "Australian"
                },
            new Pilot()
                {
                    Id = new Guid("76031F5A-3682-45AC-AFAC-8F31440CD863"),
                    Code = "ALO",
                  TeamId = 1,
                    Number = "27",
                    Name = "Alonso",
                    Nationality = "Spanish"
                },
            new Pilot()
                {
                    Id = new Guid("649C17E2-1FF7-4AD4-853F-103D7A24D937"),
                    Code = "BUT",
                  TeamId = 1,
                    Number = "28",
                    Name = "Jenson Button",
                    Nationality = "British"
                },
            new Pilot()
                {
                    Id = new Guid("5E76AEE7-0B54-476B-9285-91EB98FE41A1"),
                    Code = "DAV",
                    Number = "29",
                    Name = "Anthony Davidson",
                    Nationality = "British"
                },
            new Pilot()
                {
                    Id = new Guid("C36E5C5A-355B-4F65-B6F3-814BBA005EB9"),
                    Code = "VET",
                  TeamId = 1,
                    Number = "30",
                    Name = "Sebastian Vettel",
                    Nationality = "German"
                },
            new Pilot()
                {
                    Id = new Guid("8C4750CB-A810-4344-A2CE-ED82A02A6CC1"),
                    Code = "FIS",
                    Number = "31",
                  TeamId = 1,
                    Name = "Giancarlo Fisichella",
                    Nationality = "Italian"
                },
            new Pilot()
                {
                    Id = new Guid("265F95BD-189E-4286-9990-F6D212CB2BD5"),
                    Code = "BAR",
                    Number = "32",
                  TeamId = 2,
                    Name = "Rubens Barichello",
                    Nationality = "Brazilian"
                },
            new Pilot()
                {
                    Id = new Guid("3AB659D6-8DF9-40C6-ABAC-D5E26B1E3F56"),
                    Code = "SCH",
                    Number = "33",
                  TeamId = 2,
                    Name = "Michael Schumacher",
                    Nationality = "German"
                }
        */
        };
    }
}
