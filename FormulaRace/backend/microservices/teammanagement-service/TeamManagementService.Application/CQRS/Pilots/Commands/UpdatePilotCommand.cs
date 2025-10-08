using MediatR;
using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagementService.Application.CQRS.Pilots.Commands;

public record UpdatePilotCommand(int Id, PilotUpdateDto UpdateDto) : IRequest;