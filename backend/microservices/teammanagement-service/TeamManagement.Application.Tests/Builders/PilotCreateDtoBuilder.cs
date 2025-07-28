using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagement.Application.Tests.Builders;

public class PilotCreateDtoBuilder
{
    private string _name = "Default Pilot";
    private string _number = "00";
    private string _code = "DEF";
    private string _nationality = "Defaultland";
    private int _teamId = 1;

    public PilotCreateDtoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PilotCreateDtoBuilder WithTeamId(int teamId)
    {
        _teamId = teamId;
        return this;
    }

    public PilotCreateDto Build()
    {
        return new PilotCreateDto(_name, _number, _code, _nationality, _teamId);
    }
}
