namespace TeamManagementService.Application.Dtos.Pilots;

public record PilotDeleteDto(int Id, string Name, string Number, string Code, string Nationality, int TeamId);