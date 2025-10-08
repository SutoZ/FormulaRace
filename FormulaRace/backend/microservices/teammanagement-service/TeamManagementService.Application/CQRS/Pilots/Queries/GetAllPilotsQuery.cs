using MediatR;
using OneOf;
using OneOf.Types;
using Race.Shared.Utilities.Paging;
using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagementService.Application.CQRS.Pilots.Queries;

public record GetAllPilotsQuery(PagerParameters PagerParameters, PilotFilterDto FilterDto) : IRequest<OneOf<IPagedList<PilotListDto>, NotFound, Error>>;