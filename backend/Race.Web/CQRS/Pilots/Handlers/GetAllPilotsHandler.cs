using MediatR;
using Race.Shared.Paging;
using Race.Repo.Dtos.Pilots;
using Race.Service.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using Race.Web.CQRS.Pilots.Queries;

namespace Race.Web.CQRS.Pilots.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class GetAllPilotsHandler(IPilotService pilotSerice) : IRequestHandler<GetAllPilotsQuery, IPagedList<PilotListDto>>
{
    public async Task<IPagedList<PilotListDto>> Handle(GetAllPilotsQuery request, CancellationToken cancellationToken)
    {
        return await pilotSerice.GetAllAsync(request.PagerParameters, cancellationToken);
    }
}
