using MediatR;
using Race.Shared.Paging;
using Race.Repo.Dtos.Teams;

namespace Race.Web.CQRS.Teams.Queries;

public record GetAllTeamsQuery(PagerParameters PagerParameters) : IRequest<PagedList<TeamListDto>>;