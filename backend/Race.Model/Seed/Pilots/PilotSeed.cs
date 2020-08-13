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
                  Id = new Guid("595A50EC-75A6-434C-98F7-DC28A5AD6C00"),
                  Code = "HAM",
                  Number = "44",
                  Name = "Lewis Hamilton",
                  Nationality = "British"
                },
                new Pilot()
                {
                    Id = new Guid("C274F0F1-9ABA-41EF-B271-E671074D8AA2"),
                    Code = "HEI",
                    Number = "50",
                    Name = "Nick Heidfeld",
                    Nationality = "German"
                },
                new Pilot()
                {
                    Id = new Guid("3DC612C0-5582-46BA-B2FA-6D72493E130D"),
                    Code = "ROS",
                    Number = "6",
                    Name = "Nico Rosberg",
                    Nationality = "German"
                },
                new Pilot()
                {
                    Id = new Guid("AF592A6B-20F2-45EB-B942-B41B899DD887"),
                    Code = "RAI",
                    Number = "14",
                    Name = "Kimi Raikonnen",
                    Nationality = "Finnish"
                },
                new Pilot()
                {
                    Id = new Guid("F91F48D4-51EA-4F44-95EC-182B8E7B029C"),
                    Code = "KUB",
                    Number = "19",
                    Name = "Robert Kubica",
                    Nationality = "Polish"
                },
            new Pilot()
                {
                    Id = new Guid("6B63D3A4-8A19-41E1-B895-60EA9D501C0A"),
                    Code = "GLO",
                    Number = "20",
                    Name = "Timo Glock",
                    Nationality = "German"
                },
            new Pilot()
                {
                    Id = new Guid("A9E5BA71-62F9-49D6-977A-647BDD3B7037"),
                    Code = "SAT",
                    Number = "21",
                    Name = "Tacuma Sato",
                    Nationality = "Japanese"
                },
            new Pilot()
                {
                    Id = new Guid("AEB5A563-9B9F-44DB-A56E-D7404ADE9BE3"),
                    Code = "MAS",
                    Number = "22",
                    Name = "Felipe Massa",
                    Nationality = "Brazilian"
                },
            new Pilot()
                {
                    Id = new Guid("918AD535-B482-40AF-ACB5-0219418B51FE"),
                    Code = "COU",
                    Number = "23",
                    Name = "David Coulthard",
                    Nationality = "British"
                },
            new Pilot()
                {
                    Id = new Guid("3F179E30-F470-4253-A90C-FDC5CEF4F76E"),
                    Code = "TRU",
                    Number = "24",
                    Name = "Jarno Trulli",
                    Nationality = "Italian"
                },
            new Pilot()
                {
                    Id = new Guid("8E1B2BE1-4A7C-4EC2-9072-A0AE97642605"),
                    Code = "SUT",
                    Number = "25",
                    Name = "Adrian Sutil",
                    Nationality = "German"
                },
            new Pilot()
                {
                    Id = new Guid("30E3C76B-106A-42AF-880A-0E7A4B9D18E8"),
                    Code = "WEB",
                    Number = "26",
                    Name = "Mark Webber",
                    Nationality = "Australian"
                },
            new Pilot()
                {
                    Id = new Guid("76031F5A-3682-45AC-AFAC-8F31440CD863"),
                    Code = "ALO",
                    Number = "27",
                    Name = "Alonso",
                    Nationality = "Spanish"
                },
            new Pilot()
                {
                    Id = new Guid("649C17E2-1FF7-4AD4-853F-103D7A24D937"),
                    Code = "BUT",
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
                    Number = "30",
                    Name = "Sebastian Vettel",
                    Nationality = "German"
                },
            new Pilot()
                {
                    Id = new Guid("8C4750CB-A810-4344-A2CE-ED82A02A6CC1"),
                    Code = "FIS",
                    Number = "31",
                    Name = "Giancarlo Fisichella",
                    Nationality = "Italian"
                },
            new Pilot()
                {
                    Id = new Guid("265F95BD-189E-4286-9990-F6D212CB2BD5"),
                    Code = "BAR",
                    Number = "32",
                    Name = "Rubens Barichello",
                    Nationality = "Brazilian"
                },
            new Pilot()
                {
                    Id = new Guid("3AB659D6-8DF9-40C6-ABAC-D5E26B1E3F56"),
                    Code = "SCH",
                    Number = "33",
                    Name = "Michael Schumacher",
                    Nationality = "German"
                }
        };
    }
}
