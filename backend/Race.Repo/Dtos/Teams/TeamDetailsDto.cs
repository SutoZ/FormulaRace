using Race.Model.Models;
using Race.Repo.Dtos.Pilots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Race.Repo.Dtos.Teams
{
    public class TeamDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfFoundation { get; set; }
        public string OwnerName { get; set; }
        public int ChampionShipPoints { get; set; }
        public List<PilotDetailsDto> Pilots { get; set; }

        public TeamDetailsDto(Team team)
        {
            Id = team.Id;
            Name = team.Name;
            DateOfFoundation = team.DateOfFoundation;
            OwnerName = team.OwnerName;
            ChampionShipPoints = team.ChampionShipPoints;
            Pilots = team.Pilots != null ? team.Pilots.Select(ent => new PilotDetailsDto(ent)).ToList() : null;
        }
    }
}
