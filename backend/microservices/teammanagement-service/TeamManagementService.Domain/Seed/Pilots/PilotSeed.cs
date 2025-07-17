using TeamManagementService.Domain.Models;

namespace TeamManagementService.Domain.Seed.Pilots;

public class PilotSeed
{
    public Pilot[] Entities =>
    [
            new Pilot
            {
              Code = "HAM",
              Number = "44",
              Name = "Lewis Hamilton",
              TeamId = 2,
              Nationality = "British",
              CreatedBy = "System",
              CreatedAt = DateTimeOffset.UtcNow,
              Active = true
            },
               new Pilot()
            {
                Code = "OCO",
                Number = "50",
                Name = "Esteban Occon",
                TeamId = 4,
                Nationality = "France",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
            },
             new Pilot()
      {
                Code = "ROS",
                TeamId = 1,
                Number = "6",
                Name = "Nico Rosberg",
                Nationality = "German",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
        },
            new Pilot()
            {
                Code = "RAI",
                TeamId = 2,
                Number = "14",
                Name = "Kimi Raikonnen",
                Nationality = "Finnish",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
            },
                new Pilot()
            {
                Code = "RUS",
                Number = "19",
                Name = "George Russel",
                TeamId = 5,
                Nationality = "English",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
            },
                new Pilot()
            {
                Code = "LAT",
                TeamId = 5,
                Number = "20",
                Name = "Nicholas Latifi",
                Nationality = "Canadian",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
            },
            new Pilot()
            {
                Code = "NOR",
                TeamId = 3,
                Number = "24",
                Name = "Lando Norris",
                Nationality = "Italian",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
            },
            new Pilot()
            {
                Code = "SAI",
                TeamId = 3,
                Number = "25",
                Name = "Carlos Sainz",
                Nationality = "Spanish",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
            },
             new Pilot()
             {
                Code = "RIC",
                Number = "32",
                TeamId = 4,
                Name = "Daniel Ricciardo",
                Nationality = "Australian",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
            },

                 new Pilot()
            {
                Code = "GAS",
                Number = "29",
                TeamId = 6,
                Name = "Pierre Gasly",
                Nationality = "French",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
            },
                new Pilot()
            {
                Code = "KVY",
                Number = "31",
                TeamId = 6,
                Name = "Daniil Kvyat",
                Nationality = "Russian",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
            },
                new Pilot()
            {
                Code = "VET",
                TeamId = 1,
                Number = "30",
                Name = "Sebastian Vettel",
                Nationality = "German",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
            },
                new Pilot()
            {
                Code = "SCH",
                Number = "33",
                TeamId = 1,
                Name = "Michael Schumacher",
                Nationality = "German",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
            },
                new Pilot
            {
              Code = "BOT",
              Number = "70",
              Name = "Walteri Bottas",
              TeamId = 2,
              Nationality = "Finnish",
              CreatedBy = "System",
              CreatedAt = DateTimeOffset.UtcNow,
              Active = true
            },
    ];
}