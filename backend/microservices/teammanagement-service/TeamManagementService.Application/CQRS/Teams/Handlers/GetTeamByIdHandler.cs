using MediatR;
using OneOf;
using OneOf.Types;
using TeamManagementService.Application.CQRS.Teams.Queries;
using TeamManagementService.Application.Dtos.Teams;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.CQRS.Teams.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class GetTeamByIdHandler(ITeamService teamService) : IRequestHandler<GetTeamByIdQuery, OneOf<TeamDetailsDto, NotFound, Error>>
{
    public async Task<OneOf<TeamDetailsDto, NotFound, Error>> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await teamService.GetByIdAsync(request.Id, cancellationToken);

        if (result is null)
            return new NotFound();

        return result;
    }
}