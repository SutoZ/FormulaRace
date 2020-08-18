using Race.Repo.Dtos.Teams;
using Race.Repo.Interfaces;
using Race.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Race.Service.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository repository;

        public TeamService(ITeamRepository repository)
        {
            this.repository = repository;
        }
        public async Task<int> CreateTeamAsync(TeamCreateDto createDto)
        {
            return await repository.InsertAsync(createDto);
        }

        public Task<int> DeleteTeamAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TeamListDto>> GetAllTeamAsync()
        {
            return await repository.GetAllTeamAsync();
        }

        public async Task<TeamDetailsDto> GetTeamByIdAsync(int id)
        {
            return await repository.GetTeamByIdAsync(id);
        }

        public async Task UpdateTeamAsync(int id, TeamUpdateDto updateDto)
        {
            await repository.UpdateTeamAsync(id, updateDto);
        }
    }
}
