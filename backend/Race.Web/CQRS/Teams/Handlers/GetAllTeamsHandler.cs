using MediatR;
using Race.Repo.Dtos.Teams;
using Race.Service.Interfaces;
using Race.Shared.Paging;
using Race.Web.CQRS.Teams.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Web.CQRS.Teams.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class GetAllTeamsHandler(ITeamService teamService) : IRequestHandler<GetAllTeamsQuery, IPagedList<TeamListDto>>
{
    public async Task<IPagedList<TeamListDto>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        var result = await teamService.GetAllAsync(request.PagerParameters, cancellationToken);
        return result;
    }
}