using TeamManagementService.Domain.Models;

namespace TeamManagement.Application.Helper.Builders;

public class PilotBuilder
{
    private string _name = "Default Pilot";
    private string _number = "00";
    private string _code = "DEF";
    private string _nationality = "Defaultland";
    private int _teamId = 1;
    private Team _team = new TeamBuilder().Build();

    public PilotBuilder()
    {
        _team = new Team { Id = _teamId, Name = "Default Team" };
    }

    public PilotBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PilotBuilder WithNumber(string number)
    {
        _number = number;
        return this;
    }

    public PilotBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public PilotBuilder WithNationality(string nationality)
    {
        _nationality = nationality;
        return this;
    }

    public PilotBuilder WithTeam(Team team)
    {
        _team = team;
        _teamId = team.Id;
        return this;
    }

    public PilotBuilder WithTeamId(int teamId)
    {
        _teamId = teamId;
        return this;
    }

    public Pilot Build()
    {
        var pilot = new Pilot(_name, _number, _code, _nationality, _teamId)
        {
            Team = _team,
            CreatedAt = DateTime.UtcNow,
            Active = true
        };
        return pilot;
    }
}