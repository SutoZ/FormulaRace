using MediatR;
using Race.Service.Interfaces;
using Race.Web.CQRS.Teams.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Web.CQRS.Teams.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class DeleteTeamHandler(ITeamService teamService) : IRequestHandler<DeleteTeamCommand>
{
    public async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        await teamService.DeleteAsync(request.Id, cancellationToken);
    }
}