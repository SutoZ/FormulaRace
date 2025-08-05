using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Dtos.Pilots;

public record PilotDetailsDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Number { get; set; }
    public string? Code { get; set; }
    public string? Nationality { get; set; }

    public static PilotDetailsDto FromPilot(Pilot pilot) => new()
    {
        Id = pilot.Id,
        Code = pilot.Code,
        Name = pilot.Name,
        Number = pilot.Number,
        Nationality = pilot.Nationality,
    };
}