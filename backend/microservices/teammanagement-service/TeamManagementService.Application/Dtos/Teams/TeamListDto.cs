using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Dtos.Teams;

public record TeamListDto
{
    public string? Name { get; init; }
    public DateTime? DateOfFoundation { get; init; }
    public string? OwnerName { get; init; }
    public int? ChampionShipPoints { get; init; }    

    public static TeamListDto FromTeam(Team team) => new()
    {
        Name = team.Name,
        DateOfFoundation = team.DateOfFoundation,
        OwnerName = team.OwnerName,
        ChampionShipPoints = team.ChampionShipPoints
    };
}