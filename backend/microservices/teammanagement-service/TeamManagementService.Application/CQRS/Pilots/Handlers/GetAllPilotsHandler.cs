using MediatR;
using OneOf;
using OneOf.Types;
using Race.Shared.Utilities.Paging;
using TeamManagementService.Application.CQRS.Pilots.Queries;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.CQRS.Pilots.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT REMOVE this class or its namespace
public class GetAllPilotsHandler(IPilotService pilotSerice) : IRequestHandler<GetAllPilotsQuery, OneOf<IPagedList<PilotListDto>, NotFound, Error>>
{
    public async Task<OneOf<IPagedList<PilotListDto>, NotFound, Error>> Handle(GetAllPilotsQuery request, CancellationToken cancellationToken)
    {
        return await pilotSerice.GetAllAsync(request.PagerParameters, request.FilterDto, cancellationToken);
    }
}
