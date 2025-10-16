using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Dtos.Teams;

namespace TeamManagement.Application.Helper.Builders;

public class PilotListDtoBuilder
{
    private int? _id = 1;
    private string? _name = "Default Pilot";
    private string? _number = "00";
    private string? _code = "DEF";
    private string? _nationality = "Defaultland";
    private TeamListDto? _teamListDto = null;

    public PilotListDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public PilotListDtoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PilotListDtoBuilder WithNumber(string number)
    {
        _number = number;
        return this;
    }

    public PilotListDtoBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public PilotListDtoBuilder WithNationality(string nationality)
    {
        _nationality = nationality;
        return this;
    }

    public PilotListDtoBuilder WithTeamListDto(TeamListDto teamListDto)
    {
        _teamListDto = teamListDto;
        return this;
    }

    public PilotListDto Build() => new()
    {
        Id = _id,
        Name = _name,
        Number = _number,
        Code = _code,
        Nationality = _nationality,
        TeamListDto = _teamListDto
    };
}
