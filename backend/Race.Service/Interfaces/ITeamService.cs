using Race.Repo.Dtos.Teams;
using Race.Shared.Paging;
using System.Threading.Tasks;

namespace Race.Service.Interfaces
{
    public interface ITeamService
    {
        Task<PagedList<TeamListDto>> GetAllTeamAsync(int pageIndex, int pageSize, string sortColumn, string sortOrder);
        Task<TeamDetailsDto> GetTeamByIdAsync(int id);
        Task<int> CreateTeamAsync(TeamCreateDto createDto);
        Task UpdateTeamAsync(int id, TeamUpdateDto updateDto);
        Task<int> DeleteTeamAsync(int id);
    }
}
