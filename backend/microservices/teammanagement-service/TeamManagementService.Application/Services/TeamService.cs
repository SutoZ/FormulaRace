using Race.Shared.Utilities.Paging;
using TeamManagementService.Application.Dtos.Teams;
using TeamManagementService.Application.Interfaces;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.Services;

public class TeamService(IUnitOfWork uow) : ITeamService
{
    public async Task<int> CreateAsync(TeamCreateDto createDto, CancellationToken token)
    {
        var result = await uow.Team.CreateAsync(createDto, token);
        return result;
    }

    public async Task<int> DeleteAsync(int id, CancellationToken token)
    {
        var result = await uow.Team.DeleteAsync(id, token);
        return result;
    }

    public async Task<IPagedList<TeamListDto>> GetAllAsync(PagerParameters pagerParameters, CancellationToken token)
    {
        var result = await uow.Team.GetAllAsync(pagerParameters, token);
        return result;
    }

    public async Task<TeamDetailsDto> GetByIdAsync(int id, CancellationToken token)
    {
        var result = await uow.Team.GetByIdAsync(id, token);
        return result;
    }

    public async Task UpdateAsync(int id, TeamUpdateDto updateDto, CancellationToken token)
    {
        await uow.Team.UpdateAsync(id, updateDto, token);
    }
}