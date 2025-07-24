using MediatR;
using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagementService.Application.CQRS.Pilots.Commands;

public record CreatePilotCommand(PilotCreateDto Pilot) : IRequest<PilotListDto>;