using MediatR;
using OneOf;
using OneOf.Types;
using Race.Repo.Dtos.Pilots;

namespace Race.Web.CQRS.Pilots.Queries;

public record GetPilotByIdQuery(int Id) : IRequest<OneOf<PilotDetailsDto, NotFound, Error>>;