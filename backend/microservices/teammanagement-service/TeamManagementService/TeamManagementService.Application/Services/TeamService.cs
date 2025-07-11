using Race.Shared.Utilities.Paging;
using TeamManagementService.Application.Dtos.Teams;
using TeamManagementService.Application.Interfaces.Repositories;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.Services;

public class TeamService(ITeamRepository repository) : ITeamService
{
    public async Task<int> CreateAsync(TeamCreateDto createDto, CancellationToken token)
    {
        return await repository.InsertAsync(createDto, token);
    }

    public Task<int> DeleteAsync(int id, CancellationToken token)
    {
        return repository.DeleteAsync(id, token);
    }

    public async Task<IPagedList<TeamListDto>> GetAllAsync(PagerParameters pagerParameters, CancellationToken token)
    {
        return await repository.GetAllAsync(pagerParameters, token);
    }

    public async Task<TeamDetailsDto> GetByIdAsync(int id, CancellationToken token)
    {
        return await repository.GetByIdAsync(id, token);
    }

    public async Task UpdateAsync(int id, TeamUpdateDto updateDto, CancellationToken token)
    {
        await repository.UpdateAsync(id, updateDto, token);
    }
}