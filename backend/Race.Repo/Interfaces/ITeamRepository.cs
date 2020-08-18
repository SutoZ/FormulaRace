using Race.Repo.Dtos.Teams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Race.Repo.Interfaces
{
    public interface ITeamRepository
    {
        Task<int> InsertAsync(TeamCreateDto entity);
        Task<List<PilotListDto>> GetAllPilotAsync();
        Task<PilotDetailsDto> GetPilotAsync(int id);
        Task UpdatePilotAsync(int id, PilotUpdateDto updateDto);
        Task<int> DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
