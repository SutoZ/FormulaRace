using Race.Repo.Dtos.Pilots;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Race.Repo.Interfaces
{
    public interface IPilotRepository
    {
        Task<Guid> InsertAsync(PilotCreateDto entity);
        Task<List<PilotListDto>> GetAllPilotAsync();
        Task<PilotDetailsDto> GetPilotAsync(Guid id);
        Task UpdatePilotAsync(Guid id, PilotUpdateDto updateDto);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}
