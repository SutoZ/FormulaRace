using MediatR;
using OneOf;
using System.Threading;
using System.Threading.Tasks;
using OneOf.Types;
using TeamManagementService.Application.CQRS.Pilots.Commands;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.CQRS.Pilots.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class DeletePilotHandler(IPilotService pilotService) : IRequestHandler<DeletePilotCommand, OneOf<int, NotFound, Error>>
{
    public async Task<OneOf<int, NotFound, Error>> Handle(DeletePilotCommand request, CancellationToken cancellationToken)
    {
        var result = await pilotService.DeleteAsync(request.Id, cancellationToken);

        if (result is 0)
            return new NotFound();

        return result;
    }
}