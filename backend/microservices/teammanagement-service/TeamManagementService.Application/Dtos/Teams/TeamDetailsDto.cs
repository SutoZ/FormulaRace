using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Dtos.Teams;

public record TeamDetailsDto
{
    public string? Name { get; set; }
    public DateTimeOffset? DateOfFoundation { get; set; }
    public string? OwnerName { get; set; }
    public int? ChampionShipPoints { get; set; }
    public List<PilotDetailsDto> Pilots { get; set; } = [];

    public static TeamDetailsDto FromTeam(Team team) => new()
    {
        Name = team.Name,
        DateOfFoundation = team.DateOfFoundation,
        OwnerName = team.OwnerName,
        ChampionShipPoints = team.ChampionShipPoints,
        Pilots = team.Pilots is not null ? team.Pilots.ConvertAll(PilotDetailsDto.FromPilot) : []
    };
}