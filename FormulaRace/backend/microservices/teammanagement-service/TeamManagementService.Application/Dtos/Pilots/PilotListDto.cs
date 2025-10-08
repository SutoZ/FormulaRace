using TeamManagementService.Application.Dtos.Teams;
using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Dtos.Pilots;

public record PilotListDto
{
    public int? Id { get; set; }
    public string? Name { get; init; }
    public string? Number { get; init; }
    public string? Code { get; init; }
    public string? Nationality { get; set; }
    public TeamListDto? TeamListDto { get; set; }

    public static PilotListDto FromPilot(Pilot pilot) => new()
    {
        Id = pilot.Id,
        Name = pilot.Name,
        Number = pilot.Number,
        Code = pilot.Code,
        Nationality = pilot.Nationality,
        TeamListDto = pilot.Team is null ? null : TeamListDto.FromTeam(pilot.Team)
    };
}