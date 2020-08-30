using Race.Repo.Dtos.Teams;
using Race.Shared.Paging;
using System.Threading.Tasks;

namespace Race.Repo.Interfaces
{
    public interface ITeamRepository
    {
        Task<int> InsertAsync(TeamCreateDto entity);
        Task<PagedList<TeamListDto>> GetAllTeamAsync(int pageIndex, int pageSize, string sortColumn, string sortOrder);
        Task<TeamDetailsDto> GetTeamByIdAsync(int id);
        Task UpdateTeamAsync(int id, TeamUpdateDto updateDto);
        Task<int> DeleteAsync(int id);
    }
}
