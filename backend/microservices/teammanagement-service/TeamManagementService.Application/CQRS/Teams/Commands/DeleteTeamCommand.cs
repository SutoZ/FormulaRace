using MediatR;

namespace TeamManagementService.Application.CQRS.Teams.Commands;

public record DeleteTeamCommand(int Id) : IRequest;