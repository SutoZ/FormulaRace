using TeamManagementService.Application.Interfaces;
using TeamManagementService.Application.Interfaces.Repositories;
using TeamManagementService.Infrastructure.ApplicationContext;

namespace TeamManagementService.Infrastructure.UoW;

public class UnitOfWork(RaceContext context, IPilotRepository pilotRepository, ITeamRepository teamRepository) : IUnitOfWork
{
    public IPilotRepository Pilot { get; set; } = pilotRepository;
    public ITeamRepository Team { get; set; } = teamRepository;
    public async Task<int> SaveChangesAsync(CancellationToken token = default) => await context.SaveChangesAsync(token);

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
}