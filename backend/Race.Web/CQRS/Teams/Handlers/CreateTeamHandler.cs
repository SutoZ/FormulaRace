using MediatR;
using Race.Service.Interfaces;
using Race.Web.CQRS.Teams.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Web.CQRS.Teams.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class CreateTeamHandler(ITeamService teamService) : IRequestHandler<CreateTeamCommand, int>
{
    public async Task<int> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var result = await teamService.CreateAsync(request.Team, cancellationToken);
        return result;
    }
}