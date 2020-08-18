using Race.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Race.Model.Seed.Pilots
{
   public class TeamsSeed
    {
        public Team[] Entities = new Team[]
        {
            new Team
            {
                Id = 1,
                ChampionShipPoints = 20,
                Name = "Ferrari",
                DateOfFoundation= new DateTime(1950,04,04),
                OwnerName = "Teszt Random"
            },
            new Team
            {
                Id = 2,
                ChampionShipPoints = 30,
                Name = "Mercedes",
                DateOfFoundation= new DateTime(1970,04,04),
                OwnerName = "Teszt Random 23"
            }
        };
    }
}
