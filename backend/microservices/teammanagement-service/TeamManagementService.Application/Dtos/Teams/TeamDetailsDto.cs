using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Dtos.Teams;

public record TeamDetailsDto(int Id, string Name, DateTime DateOfFoundation, string OwnerName, int ChampionShipPoints, List<PilotDetailsDto> Pilots)
{
    public static TeamDetailsDto FromTeam(Team team) => new(
        team.Id,
        team.Name,
        team.DateOfFoundation,
        team.OwnerName,
        team.ChampionShipPoints,
        team.Pilots != null ? team.Pilots.ConvertAll(PilotDetailsDto.FromPilot) : []);
}