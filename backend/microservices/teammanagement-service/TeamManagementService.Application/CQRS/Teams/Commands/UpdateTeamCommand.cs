using MediatR;
using TeamManagementService.Application.Dtos.Teams;

namespace TeamManagementService.Application.CQRS.Teams.Commands;

public record UpdateTeamCommand(int Id, TeamUpdateDto UpdateDto) : IRequest;