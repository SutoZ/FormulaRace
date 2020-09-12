using Race.Repo.Dtos.Teams;
using Race.Shared.Paging;
using System.Threading.Tasks;

namespace Race.Repo.Interfaces
{
    public interface ITeamRepository
    {
        Task<int> InsertAsync(TeamCreateDto entity);
        Task<IPagedList<TeamListDto>> GetAllTeamAsync(
            int pageIndex,
            int pageSize,
            string sortColumn = null,
            string sortOrder = null,
            string filterColumn = null,
            string filterQuery = null);

        Task<TeamDetailsDto> GetTeamByIdAsync(int id);
        Task UpdateTeamAsync(int id, TeamUpdateDto updateDto);
        Task<int> DeleteAsync(int id);
    }
}
