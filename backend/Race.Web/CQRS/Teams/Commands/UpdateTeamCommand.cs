using MediatR;
using Race.Repo.Dtos.Teams;

namespace Race.Web.CQRS.Teams.Commands;

public record UpdateTeamCommand(int Id, TeamUpdateDto UpdateDto) : IRequest;