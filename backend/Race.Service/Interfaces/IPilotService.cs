using Race.Repo.Dtos;
using Race.Repo.Dtos.Pilots;
using Race.Shared.Paging;
using System.Threading.Tasks;

namespace Race.Service.Interfaces
{
    public interface IPilotService
    {
        Task<IPagedList<PilotListDto>> GetAllPilotAsync(PagerDto dto);
        Task<PilotDetailsDto> GetPilotAsync(int id);
        Task<int> CreatePilotAsync(PilotCreateDto createDto);
        Task UpdatePilotAsync(int id, PilotUpdateDto updateDto);
        Task<int> DeletePilotAsync(int id);
    }
}
