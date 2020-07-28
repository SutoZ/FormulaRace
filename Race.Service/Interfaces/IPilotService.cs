using Race.Repo.Dtos.Pilots;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Race.Service.Interfaces
{
    public interface IPilotService
    {
        Task<List<PilotListDto>> GetAllPilot();
        Task<PilotDetailsDto> GetPilotAsync(Guid id);
        Task<Guid> CreatePilotAsync(PilotCreateDto createDto);
        Task UpdatePilotAsync(Guid id, PilotUpdateDto updateDto);
        Task<Guid> DeletePilotAsync(Guid id);
    }
}
