using MediatR;
using TeamManagementService.Application.Dtos.Teams;

namespace TeamManagementService.Application.CQRS.Teams.Commands;

public record CreateTeamCommand(TeamCreateDto Team) : IRequest<int>;