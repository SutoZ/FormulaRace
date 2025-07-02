using Race.Repo.Dtos.Teams;
using Race.Shared.Paging;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Repo.Interfaces;

public interface ITeamRepository
{
    Task<int> InsertAsync(TeamCreateDto entity, CancellationToken token);
    Task<IPagedList<TeamListDto>> GetAllAsync(PagerParameters pagerParameters, CancellationToken token);
    Task<TeamDetailsDto> GetByIdAsync(int id, CancellationToken token);
    Task UpdateAsync(int id, TeamUpdateDto updateDto, CancellationToken token);
    Task<int> DeleteAsync(int id, CancellationToken token);
    Task<int> CreateAsync(TeamCreateDto teamCreateDto, CancellationToken token);
}