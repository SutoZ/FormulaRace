using MediatR;
using OneOf;
using OneOf.Types;

namespace Race.Web.CQRS.Pilots.Commands;

public record DeletePilotCommand(int Id) : IRequest<OneOf<int, NotFound, Error>>;