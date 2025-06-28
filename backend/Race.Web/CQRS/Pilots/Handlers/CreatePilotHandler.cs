using MediatR;
using Race.Service.Interfaces;
using Race.Web.CQRS.Pilots.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Web.CQRS.Pilots.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class CreatePilotHandler(IPilotService pilotService) : IRequestHandler<CreatePilotCommand, int>
{
    public async Task<int> Handle(CreatePilotCommand request, CancellationToken cancellationToken)
    {
        var result = await pilotService.CreateAsync(request.Pilot, cancellationToken);
        return result;
    }
}