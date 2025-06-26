using Race.Model.Models;
using System;

namespace Race.Repo.Dtos.Teams;

public record TeamCreateDto(string Name, DateTime DateOfFoundation, string OwnerName, int ChampionShipPoints)
{
    public static TeamCreateDto FromTeam(Team team) => new(
        team.Name,
        team.DateOfFoundation,
        team.OwnerName,
        team.ChampionShipPoints);
}