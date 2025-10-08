using TeamManagementService.Application.Interfaces.Repositories;

namespace TeamManagementService.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IPilotRepository Pilot { get; set; }
    public ITeamRepository Team { get; set; }
    Task<int> SaveChangesAsync(CancellationToken token = default);
}