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
                  TeamId = 2,
                  Nationality = "British"
                },
                   new Pilot()
                {
                    Id = 2,
                    Code = "OCO",
                    Number = "50",
                    Name = "Esteban Occon",
                    TeamId = 4,
                    Nationality = "France"
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
                    TeamId = 2,
                    Number = "14",
                    Name = "Kimi Raikonnen",
                    Nationality = "Finnish"
                },
                    new Pilot()
                {
                    Id = 5,
                    Code = "RUS",
                    Number = "19",
                    Name = "Gerogre Russel",
                    TeamId = 5,
                    Nationality = "English"
                },
                    new Pilot()
                {
                    Id = 6,
                    Code = "LAT",
                    TeamId = 5,
                    Number = "20",
                    Name = "Nicholas Latifi",
                    Nationality = "Canadian"
                },
                new Pilot()
                {
                    Id = 7,
                    Code = "NOR",
                    TeamId = 3,
                    Number = "24",
                    Name = "Lando Norris",
                    Nationality = "Italian"
                },
                new Pilot()
                {
                    Id = 8,
                    Code = "SAI",
                    TeamId = 3,
                    Number = "25",
                    Name = "Carlos Sainz",
                    Nationality = "Spanish"
                },
                 new Pilot()
                 {
                    Id = 9,
                    Code = "RIC",
                    Number = "32",
                    TeamId = 4,
                    Name = "Daniel Ricciardo",
                    Nationality = "Australian"
                },
                 
            new Pilot()
                {
                    Id = 10,
                    Code = "GAS",
                    Number = "29",
                    TeamId = 6,
                    Name = "Pierre Gasly",
                    Nationality = "French"
                },
                    new Pilot()
                {
                    Id = 11,
                    Code = "KVY",
                    Number = "31",
                    TeamId = 6,
                    Name = "Daniil Kvyat",
                    Nationality = "Russian"
                },
                    new Pilot()
                {
                    Id = 12,
                    Code = "VET",
                    TeamId = 1,
                    Number = "30",
                    Name = "Sebastian Vettel",
                    Nationality = "German"
                },           
          new Pilot()
                {
                    Id = 13,
                    Code = "SCH",
                    Number = "33",
                    TeamId = 1,
                    Name = "Michael Schumacher",
                    Nationality = "German"
                },
            new Pilot
                {
                  Id = 14,
                  Code = "BOT",
                  Number = "70",
                  Name = "Walteri Bottas",
                  TeamId = 2,
                  Nationality = "Finnish"
                },
          
        };
    }
}
