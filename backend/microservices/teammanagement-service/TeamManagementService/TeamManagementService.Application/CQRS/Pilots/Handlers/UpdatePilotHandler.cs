using MediatR;
using TeamManagementService.Application.CQRS.Pilots.Commands;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.CQRS.Pilots.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class UpdatePilotHandler(IPilotService pilotService) : IRequestHandler<UpdatePilotCommand>
{
    public async Task Handle(UpdatePilotCommand request, CancellationToken cancellationToken)
    {
        await pilotService.UpdateAsync(request.Id, request.UpdateDto, cancellationToken);
    }
}