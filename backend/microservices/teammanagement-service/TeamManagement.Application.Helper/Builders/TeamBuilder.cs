using TeamManagementService.Domain.Models;

namespace TeamManagement.Application.Helper.Builders;

public class TeamBuilder
{
    private string _name = "Default Team";
    private int _championshipPoints = 100;
    private string _ownerName = "Default Owner";

    public TeamBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public TeamBuilder WithChampionShipPoints(int points)
    {
        _championshipPoints = points;
        return this;
    }

    public TeamBuilder WithOwnerName(string ownerName)
    {
        _ownerName = ownerName;
        return this;
    }

    public Team Build()
    {
        return new Team
        {
            Name = _name,
            OwnerName = _ownerName,
            ChampionShipPoints = _championshipPoints,
            DateOfFoundation = DateTime.UtcNow.AddYears(-5),
            Active = true
        };
    }
}