using Race.Repo.Dtos.Pilots;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Race.Service.Interfaces
{
    public interface IPilotService
    {
        Task<List<PilotListDto>> GetAllPilotAsync();
        Task<PilotDetailsDto> GetPilotAsync(int id);
        Task<int> CreatePilotAsync(PilotCreateDto createDto);
        Task UpdatePilotAsync(int id, PilotUpdateDto updateDto);
        Task<int> DeletePilotAsync(int id);
    }
}
