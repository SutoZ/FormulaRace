using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeamManagementService.Application.CQRS.Teams.Commands;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.CQRS.Teams.Handlers;

// Handler is referenced by reflection in the controllerm DO NOT remove this class or its namespace
public class UpdateTeamHandler(ITeamService teamService): IRequestHandler<UpdateTeamCommand>
{
    public async Task Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        await teamService.UpdateAsync(request.Id, request.UpdateDto, cancellationToken);
    }
}