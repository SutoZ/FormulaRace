using Race.Repo.Dtos.Pilots;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Race.Service.Interfaces
{
    public interface IPilotService
    {
        Task<List<PilotListDto>> GetAllPilot();
        Task<PilotDetailsDto> GetPilotAsync(int id);
        Task<int> InsertPilotAsync(PilotCreateDto createDto);
        Task UpdatePilotAsync(int id, PilotUpdateDto updateDto);
        Task DeletePilotAsync(int id);
    }
}
