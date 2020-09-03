using Race.Repo.Dtos.Pilots;
using Race.Shared.Paging;
using System.Threading.Tasks;

namespace Race.Service.Interfaces
{
    public interface IPilotService
    {
        Task<IPagedList<PilotListDto>> GetAllPilotAsync(
            int pageIndex,
            int pageSize,
            string sortColumn,
            string sortOrder,
            string filterColumn,
            string filterQuery);

        Task<PilotDetailsDto> GetPilotAsync(int id);
        Task CreatePilotAsync(PilotCreateDto createDto);
        Task UpdatePilotAsync(int id, PilotUpdateDto updateDto);
        Task<int> DeletePilotAsync(int id);
    }
}
