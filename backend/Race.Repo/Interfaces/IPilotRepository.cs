using Race.Repo.Dtos.Pilots;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Race.Repo.Interfaces
{
    public interface IPilotRepository
    {
        Task<int> InsertAsync(PilotCreateDto entity);
        Task<List<PilotListDto>> GetAllPilotAsync(int pageIndex = 0, int pageSize = 5);
        Task<PilotDetailsDto> GetPilotAsync(int id);
        Task UpdatePilotAsync(int id, PilotUpdateDto updateDto);
        Task<int> DeleteAsync(int id);
    }
}
