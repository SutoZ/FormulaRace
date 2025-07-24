using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Dtos.Pilots;

public record PilotCreateDto(string Name, string Number, string Code, string Nationality, int TeamId)
{
    public static PilotCreateDto FromPilot(Pilot pilot) => new(
        pilot.Name,
        pilot.Number,
        pilot.Code,
        pilot.Nationality,
        pilot.TeamId);
}