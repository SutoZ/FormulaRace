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
                Nationality = "France",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
            },
             new Pilot()
             {
                Code = "ROS",
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
                Nationality = "English",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
            },
                new Pilot()
            {
                Code = "LAT",
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
                Name = "Daniil Kvyat",
                Nationality = "Russian",
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.UtcNow,
                Active = true
            },
                new Pilot()
            {
                Code = "VET",
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
              Nationality = "Finnish",
              CreatedBy = "System",
              CreatedAt = DateTimeOffset.UtcNow,
              Active = true
            },
    ];
}