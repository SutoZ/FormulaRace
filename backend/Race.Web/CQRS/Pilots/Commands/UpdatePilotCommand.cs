using MediatR;
using Race.Repo.Dtos.Pilots;

namespace Race.Web.CQRS.Pilots.Commands;

public record UpdatePilotCommand(int Id, PilotUpdateDto UpdateDto) : IRequest;