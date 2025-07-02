using MediatR;
using Race.Service.Interfaces;
using Race.Repo.Dtos.Pilots;
using OneOf;
using System.Threading;
using System.Threading.Tasks;
using OneOf.Types;
using Race.Web.CQRS.Pilots.Queries;

namespace Race.Web.CQRS.Pilots.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class GetPilotByIdHandler(IPilotService pilotService) : IRequestHandler<GetPilotByIdQuery, OneOf<PilotDetailsDto, NotFound, Error>>
{
    public async Task<OneOf<PilotDetailsDto, NotFound, Error>> Handle(GetPilotByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await pilotService.GetByIdAsync(request.Id, cancellationToken);

        if (result is null)
            return new NotFound();

        return result;
    }
}