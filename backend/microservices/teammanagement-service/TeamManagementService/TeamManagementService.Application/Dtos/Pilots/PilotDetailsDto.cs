using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Dtos.Pilots;

public record PilotDetailsDto(int Id, string Name, string Number, string Code, string Nationality)
{
    public static PilotDetailsDto FromPilot(Pilot pilot) => new(
        pilot.Id,
        pilot.Name,
        pilot.Number,
        pilot.Code,
        pilot.Nationality);
}