using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagement.Application.Helper.Builders;

public class PilotCreateDtoBuilder
{
    private string _name = "Default Pilot";
    private string _number = "00";
    private string _code = "DEF";
    private string _nationality = "Defaultland";
    private int _teamId = 3000;
    private int _pilotId = 0;

    public PilotCreateDtoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PilotCreateDtoBuilder WithId(int id)
    {
        _pilotId = id;
        return this;
    }

    public PilotCreateDtoBuilder WithTeamId(int teamId)
    {
        _teamId = teamId;
        return this;
    }

    public PilotCreateDtoBuilder WithNumber(string number)
    {
        _number = number;
        return this;
    }

    public PilotCreateDtoBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public PilotCreateDtoBuilder WithNationality(string nationality)
    {
        _nationality = nationality;
        return this;
    }

    public PilotCreateDto Build() => new() { Id = _pilotId, Code = _code, Name = _name, Nationality = _nationality, Number = _number, TeamId = _teamId };
}