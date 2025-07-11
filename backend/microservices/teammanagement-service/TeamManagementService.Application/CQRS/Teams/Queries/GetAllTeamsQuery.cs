using MediatR;
using Race.Shared.Utilities.Paging;
using TeamManagementService.Application.Dtos.Teams;

namespace TeamManagementService.Application.CQRS.Teams.Queries;

public record GetAllTeamsQuery(PagerParameters PagerParameters) : IRequest<PagedList<TeamListDto>>;