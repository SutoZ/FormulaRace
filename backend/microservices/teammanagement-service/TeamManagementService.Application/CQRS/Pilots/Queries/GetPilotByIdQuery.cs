using MediatR;
using OneOf;
using OneOf.Types;
using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagementService.Application.CQRS.Pilots.Queries;

public record GetPilotByIdQuery(int Id) : IRequest<OneOf<PilotDetailsDto, NotFound, Error>>;