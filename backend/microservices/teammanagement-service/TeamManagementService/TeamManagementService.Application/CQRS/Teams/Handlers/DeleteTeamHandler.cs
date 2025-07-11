using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeamManagementService.Application.CQRS.Teams.Commands;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.CQRS.Teams.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class DeleteTeamHandler(ITeamService teamService) : IRequestHandler<DeleteTeamCommand>
{
    public async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        await teamService.DeleteAsync(request.Id, cancellationToken);
    }
}