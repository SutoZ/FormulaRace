using Race.Repo.Dtos.Pilots;
using Race.Shared.Paging;
using System.Threading.Tasks;

namespace Race.Repo.Interfaces
{
    public interface IPilotRepository
    {
        Task CreateAsync(PilotCreateDto entity);
        Task<IPagedList<PilotListDto>> GetAllPilotAsync(
            int pageIndex,
            int pageSize,
            string sortColumn = null,
            string sortOrder = null,
            string filterColumn = null,
            string filterQuery = "");

        Task<PilotDetailsDto> GetPilotAsync(int id);
        Task UpdatePilotAsync(int id, PilotUpdateDto updateDto);
        Task<int> DeleteAsync(int id);
    }
}
