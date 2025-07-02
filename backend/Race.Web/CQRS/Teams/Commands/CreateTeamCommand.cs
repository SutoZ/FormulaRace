using MediatR;
using Race.Repo.Dtos.Teams;

namespace Race.Web.CQRS.Teams.Commands;

public record CreateTeamCommand(TeamCreateDto Team) : IRequest<int>;