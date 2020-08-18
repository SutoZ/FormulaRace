using Race.Repo.Dtos.Teams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Race.Service.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamListDto>> GetAllTeamAsync();
        Task<TeamDetailsDto> GetTeamByIdAsync(int id);
        Task<int> CreateTeamAsync(TeamCreateDto createDto);
        Task UpdateTeamAsync(int id, TeamUpdateDto updateDto);
        Task<int> DeleteTeamAsync(int id);
    }
}
