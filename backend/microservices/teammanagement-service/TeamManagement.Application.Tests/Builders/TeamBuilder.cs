using TeamManagementService.Domain.Models;

namespace TeamManagement.Application.Tests.Builders;

public class TeamBuilder
{
    private int _id = 1;
    private string _name = "Default Team";
    private int _championshipPoints = 100;

    public TeamBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

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

    public Team Build()
    {
        return new Team
        {
            Id = _id,
            Name = _name,
            ChampionShipPoints = _championshipPoints,
            DateOfFoundation = DateTime.UtcNow.AddYears(-5),
            Active = true
        };
    }
}
