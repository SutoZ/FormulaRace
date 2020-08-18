using Race.Model.Models;
using System;

namespace Race.Repo.Dtos.Teams
{
    public class TeamCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChampionShipPoints { get; set; }
        public string OwnerName { get; set; }
        public DateTime DateOfFoundation { get; set; }

        public TeamCreateDto(Team team)
        {
            Id = team.Id;
            Name = team.Name;
            ChampionShipPoints = team.ChampionShipPoints;
            OwnerName = team.OwnerName;
            DateOfFoundation = team.DateOfFoundation;
        }

        public Team CreateModelObject() => new Team
        {
            Id = Id,
            Name = Name,
            ChampionShipPoints = ChampionShipPoints,
            OwnerName = OwnerName,
            DateOfFoundation = DateOfFoundation
        };
    }
}
