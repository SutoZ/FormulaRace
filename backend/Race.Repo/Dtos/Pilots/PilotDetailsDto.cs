using Race.Model.Models;

namespace Race.Repo.Dtos.Pilots;

public record PilotDetailsDto(int Id, string Name, string Number, string Code, string Nationality)
{
    public static PilotDetailsDto FromPilot(Pilot pilot) => new(
        pilot.Id,
        pilot.Name,
        pilot.Number,
        pilot.Code,
        pilot.Nationality);
}