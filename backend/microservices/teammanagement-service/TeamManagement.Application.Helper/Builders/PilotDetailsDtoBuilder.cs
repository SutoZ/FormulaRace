using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagement.Application.Helper.Builders;

public class PilotDetailsDtoBuilder
{
    private int _id = 1;
    private string? _name;
    private string? _number;
    private string? _code;
    private string? _nationality;

    public PilotDetailsDtoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PilotDetailsDtoBuilder WithNumber(string number)
    {
        _number = number;
        return this;
    }

    public PilotDetailsDtoBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public PilotDetailsDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public PilotDetailsDtoBuilder WithNationality(string nationality)
    {
        _nationality = nationality;
        return this;
    }

    public PilotDetailsDto Build() => new()
    {
        Id = _id,
        Name = _name,
        Number = _number,
        Code = _code,
        Nationality = _nationality
    };
}