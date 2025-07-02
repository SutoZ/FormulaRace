using MediatR;
using Race.Repo.Dtos.Pilots;

namespace Race.Web.CQRS.Pilots.Commands;

public record CreatePilotCommand(PilotCreateDto Pilot) : IRequest<int>;