using MediatR;

namespace Race.Web.CQRS.Teams.Commands;

public record DeleteTeamCommand(int Id) : IRequest;