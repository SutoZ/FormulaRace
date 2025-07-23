using Race.Shared.Utilities.Paging;
using TeamManagementService.Application.Dtos.Teams;

namespace TeamManagementService.Application.Interfaces.Repositories;

public interface ITeamRepository
{
    Task<IPagedList<TeamListDto>> GetAllAsync(PagerParameters pagerParameters, CancellationToken token);
    Task<TeamDetailsDto> GetByIdAsync(int id, CancellationToken token);
    Task UpdateAsync(int id, TeamUpdateDto updateDto, CancellationToken token);
    Task<int> DeleteAsync(int id, CancellationToken token);
    Task<int> CreateAsync(TeamCreateDto teamCreateDto, CancellationToken token);
}