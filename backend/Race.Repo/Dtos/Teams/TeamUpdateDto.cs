using Race.Model.Models;
using Race.Repo.Dtos.Pilots;
using System;
using System.Collections.Generic;

namespace Race.Repo.Dtos.Teams;

public record TeamUpdateDto(string Name, DateTime DateOfFoundation, string OwnerName, int ChampionShipPoints, List<PilotUpdateDto> Pilots)
{
    public static TeamUpdateDto FromTeam(Team team) => new(
        team.Name,
        team.DateOfFoundation,
        team.OwnerName,
        team.ChampionShipPoints,
        team.Pilots != null ? team.Pilots.ConvertAll(PilotUpdateDto.FromPilot) : []);
}