using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeamManagementService.Application.CQRS.Teams.Commands;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.CQRS.Teams.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class CreateTeamHandler(ITeamService teamService) : IRequestHandler<CreateTeamCommand, int>
{
    public async Task<int> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var result = await teamService.CreateAsync(request.Team, cancellationToken);
        return result;
    }
}