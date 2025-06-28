using MediatR;
using Race.Service.Interfaces;
using Race.Web.CQRS.Teams.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Web.CQRS.Teams.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class UpdateTeamHandler(ITeamService teamService): IRequestHandler<UpdateTeamCommand>
{
    public async Task Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        await teamService.UpdateAsync(request.Id, request.UpdateDto, cancellationToken);
    }
}