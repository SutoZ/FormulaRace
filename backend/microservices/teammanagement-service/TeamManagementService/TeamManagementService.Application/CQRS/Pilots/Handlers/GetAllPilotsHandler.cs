using MediatR;
using Race.Shared.Utilities.Paging;
using TeamManagementService.Application.CQRS.Pilots.Queries;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.CQRS.Pilots.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class GetAllPilotsHandler(IPilotService pilotSerice) : IRequestHandler<GetAllPilotsQuery, IPagedList<PilotListDto>>
{
    public async Task<IPagedList<PilotListDto>> Handle(GetAllPilotsQuery request, CancellationToken cancellationToken)
    {
        return await pilotSerice.GetAllAsync(request.PagerParameters, cancellationToken);
    }
}
