using MediatR;
using TeamManagementService.Application.CQRS.Pilots.Commands;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.CQRS.Pilots.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class CreatePilotHandler(IPilotService pilotService) : IRequestHandler<CreatePilotCommand, PilotListDto>
{
    public async Task<PilotListDto> Handle(CreatePilotCommand request, CancellationToken cancellationToken)
    {
        var result = await pilotService.CreateAsync(request.Pilot, cancellationToken);
        return result;
    }
}