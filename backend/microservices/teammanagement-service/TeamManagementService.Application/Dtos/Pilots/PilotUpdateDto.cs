using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Dtos.Pilots;

public record PilotUpdateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Number { get; set; }
    public string? Code { get; set; }
    public string? Nationality { get; set; }
    public static PilotUpdateDto FromPilot(Pilot pilot) => new()
    {
        Id = pilot.Id,
        Name = pilot.Name,
        Number = pilot.Number,
        Code = pilot.Code,
        Nationality = pilot.Nationality,
    };
}