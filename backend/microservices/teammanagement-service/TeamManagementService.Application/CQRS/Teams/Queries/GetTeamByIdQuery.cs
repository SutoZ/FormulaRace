using MediatR;
using OneOf;
using OneOf.Types;
using TeamManagementService.Application.Dtos.Teams;

namespace TeamManagementService.Application.CQRS.Teams.Queries;

public record GetTeamByIdQuery(int Id) : IRequest<OneOf<TeamDetailsDto, NotFound, Error>>;