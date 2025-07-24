using TeamManagementService.Application.Dtos.Teams;
using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Dtos.Pilots;

public record PilotListDto(int Id, string Name, string Number, string Code, string Nationality, TeamListDto TeamListDto)
{
    public static PilotListDto FromPilot(Pilot pilot) => new(
            pilot.Id,
            pilot.Name,
            pilot.Number,
            pilot.Code,
            pilot.Nationality,
            pilot.Team is null ? null : new TeamListDto(
                pilot.Team.Id,
                pilot.Team.Name,
                pilot.Team.DateOfFoundation,
                pilot.Team.OwnerName,
                pilot.Team.ChampionShipPoints));
}