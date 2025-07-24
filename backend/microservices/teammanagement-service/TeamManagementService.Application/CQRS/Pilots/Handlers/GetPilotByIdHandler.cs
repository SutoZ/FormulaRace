using MediatR;
using OneOf;
using OneOf.Types;
using TeamManagementService.Application.CQRS.Pilots.Queries;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.CQRS.Pilots.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class GetPilotByIdHandler(IPilotService pilotService) : IRequestHandler<GetPilotByIdQuery, OneOf<PilotDetailsDto, NotFound, Error>>
{
    public async Task<OneOf<PilotDetailsDto, NotFound, Error>> Handle(GetPilotByIdQuery request, CancellationToken cancellationToken)
    {
        return await pilotService.GetByIdAsync(request.Id, cancellationToken);
    }
}