using MediatR;
using Race.Service.Interfaces;
using Race.Web.CQRS.Pilots.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Web.CQRS.Pilots.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class UpdatePilotHandler(IPilotService pilotService) : IRequestHandler<UpdatePilotCommand>
{
    public async Task Handle(UpdatePilotCommand request, CancellationToken cancellationToken)
    {
        await pilotService.UpdateAsync(request.Id, request.UpdateDto, cancellationToken);
    }
}