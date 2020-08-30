using Race.Model.Models;
using Race.Repo.Dtos.Pilots;
using System;
using System.Collections.Generic;
using System.Text;

namespace Race.Repo.Dtos.Teams
{
    public class TeamUpdateDto
    {
        public string Name { get; set; }
        public DateTime DateOfFoundation { get; set; }
        public string OwnerName { get; set; }
        public int ChampionShipPoints { get; set; }
        public List<PilotUpdateDto> Pilots { get; set; } = new List<PilotUpdateDto>();

        public TeamUpdateDto(Team team)
        {
            Name = team.Name;
            DateOfFoundation = team.DateOfFoundation;
            OwnerName = team.OwnerName;
            ChampionShipPoints = team.ChampionShipPoints;
            Pilots = team.Pilots != null ? new List<PilotUpdateDto>() : null;
        }

        public Team UpdateModelObject(Team team)
        {
            team.Name = Name;
            team.DateOfFoundation = DateOfFoundation;
            team.OwnerName = OwnerName;
            team.ChampionShipPoints= ChampionShipPoints;

            return team;
        }
    }
}
