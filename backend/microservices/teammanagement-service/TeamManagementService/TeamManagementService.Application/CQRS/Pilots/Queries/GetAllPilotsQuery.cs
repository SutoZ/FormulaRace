using MediatR;
using Race.Shared.Utilities.Paging;
using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagementService.Application.CQRS.Pilots.Queries;

public record GetAllPilotsQuery(PagerParameters PagerParameters) : IRequest<IPagedList<PilotListDto>>;