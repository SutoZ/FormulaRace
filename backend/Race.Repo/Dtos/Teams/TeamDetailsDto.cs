using Race.Repo.Dtos.Pilots;
using System;
using System.Collections.Generic;

namespace Race.Repo.Dtos.Teams;

public record TeamDetailsDto(int Id, string Name, DateTime DateOfFoundation, string OwnerName, int ChampionShipPoints, List<PilotDetailsDto> Pilots)
{
    public static TeamDetailsDto FromTeam(Model.Models.Team team) => new(
        team.Id,
        team.Name,
        team.DateOfFoundation,
        team.OwnerName,
        team.ChampionShipPoints,
        team.Pilots != null ? team.Pilots.ConvertAll(PilotDetailsDto.FromPilot) : []);
}