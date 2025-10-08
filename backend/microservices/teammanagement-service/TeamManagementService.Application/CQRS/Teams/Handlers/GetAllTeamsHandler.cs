using MediatR;
using Race.Shared.Utilities.Paging;
using TeamManagementService.Application.CQRS.Teams.Queries;
using TeamManagementService.Application.Dtos.Teams;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.CQRS.Teams.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class GetAllTeamsHandler(ITeamService teamService) : IRequestHandler<GetAllTeamsQuery, IPagedList<TeamListDto>>
{
    public async Task<IPagedList<TeamListDto>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        var result = await teamService.GetAllAsync(request.PagerParameters, cancellationToken);
        return result;
    }
}