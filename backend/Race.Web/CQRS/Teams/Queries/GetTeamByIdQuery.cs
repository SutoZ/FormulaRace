using MediatR;
using OneOf;
using OneOf.Types;
using Race.Repo.Dtos.Teams;

namespace Race.Web.CQRS.Teams.Queries;

public record GetTeamByIdQuery(int Id) : IRequest<OneOf<TeamDetailsDto, NotFound, Error>>;