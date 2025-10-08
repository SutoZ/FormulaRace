using MediatR;
using OneOf;
using OneOf.Types;

namespace TeamManagementService.Application.CQRS.Pilots.Commands;

public record DeletePilotCommand(int Id) : IRequest<OneOf<int, NotFound, Error>>;