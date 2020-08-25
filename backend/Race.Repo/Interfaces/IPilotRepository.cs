using Race.Repo.Dtos;
using Race.Repo.Dtos.Pilots;
using Race.Shared.Paging;
using System.Threading.Tasks;

namespace Race.Repo.Interfaces
{
    public interface IPilotRepository
    {
        Task<int> InsertAsync(PilotCreateDto entity);
        Task<IPagedList<PilotListDto>> GetAllPilotAsync(PagerDto dto);
        Task<PilotDetailsDto> GetPilotAsync(int id);
        Task UpdatePilotAsync(int id, PilotUpdateDto updateDto);
        Task<int> DeleteAsync(int id);
    }
}
