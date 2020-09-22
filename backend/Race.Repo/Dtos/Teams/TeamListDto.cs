using Race.Model.Models;
using Race.Repo.Dtos.Pilots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Race.Repo.Dtos.Teams
{
    public class TeamListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfFoundation { get; set; }
        public string OwnerName { get; set; }
        public int ChampionShipPoints { get; set; }
        //public List<PilotListDto> Pilots { get; set; }

        public TeamListDto(Team team)
        {
            Id = team.Id;
            Name = team.Name;
            DateOfFoundation = team.DateOfFoundation;
            OwnerName = team.OwnerName;
            ChampionShipPoints = team.ChampionShipPoints;
         //   Pilots = team.Pilots?.Select(ent => new PilotListDto(ent)).ToList();
        }
    }
}
