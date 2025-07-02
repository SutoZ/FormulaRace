using MediatR;
using Race.Shared.Paging;
using Race.Repo.Dtos.Pilots;

namespace Race.Web.CQRS.Pilots.Queries;

public record GetAllPilotsQuery(PagerParameters PagerParameters) : IRequest<IPagedList<PilotListDto>>;