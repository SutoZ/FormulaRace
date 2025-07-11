using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Dtos.Teams;

public record TeamCreateDto(int Id, string Name, DateTime DateOfFoundation, string OwnerName, int ChampionShipPoints)
{
    public static TeamCreateDto FromTeam(Team team) => new(
        team.Id,
        team.Name,
        team.DateOfFoundation,
        team.OwnerName,
        team.ChampionShipPoints);
}