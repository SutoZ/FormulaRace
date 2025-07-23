namespace TeamManagementService.Application.Dtos.Pilots;

public record PilotFilterDto
{
    public int? Id { get; init; }
    public string? Name { get; init; }
    public string? Number { get; init; }
    public string? Code { get; set; }
    public string? Nationality { get; set; }
}