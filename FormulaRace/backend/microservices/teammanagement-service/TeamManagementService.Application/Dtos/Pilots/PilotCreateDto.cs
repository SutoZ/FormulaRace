using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Dtos.Pilots;

public record PilotCreateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string Code { get; set; }
    public string Nationality { get; set; }
    public int TeamId { get; set; }

    public static PilotCreateDto FromPilot(Pilot pilot) => new()
    {
        Id = pilot.Id,
        Name = pilot.Name,
        Number = pilot.Number,
        Code = pilot.Code,
        Nationality = pilot.Nationality,
        TeamId = pilot.TeamId
    };
}