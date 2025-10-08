using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagement.Application.Helper.Builders;

public class PilotUpdateDtoBuilder
{
    private int _id = 1;
    private string? _name;
    private string? _number;
    private string? _code;
    private string? _nationality;

    public PilotUpdateDtoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PilotUpdateDtoBuilder WithNumber(string number)
    {
        _number = number;
        return this;
    }

    public PilotUpdateDtoBuilder WithNationality(string nationality)
    {
        _nationality = nationality;
        return this;
    }

    public PilotUpdateDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public PilotUpdateDtoBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public PilotUpdateDto Build() => new()
    {
        Id = _id,
        Name = _name,
        Number = _number,
        Code = _code,
        Nationality = _nationality
    };
}