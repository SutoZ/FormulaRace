using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Dtos.Teams;

public record TeamUpdateDto(string Name, DateTime DateOfFoundation, string OwnerName, int ChampionShipPoints, List<PilotUpdateDto> Pilots)
{
    public static TeamUpdateDto FromTeam(Team team) => new(
        team.Name,
        team.DateOfFoundation,
        team.OwnerName,
        team.ChampionShipPoints,
        team.Pilots != null ? team.Pilots.ConvertAll(PilotUpdateDto.FromPilot) : []);
}