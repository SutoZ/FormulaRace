using Race.Repo.Dtos.Teams;
using Race.Repo.Interfaces;
using Race.Service.Interfaces;
using Race.Shared.Paging;
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

        public async Task<PagedList<TeamListDto>> GetAllTeamAsync(int pageIndex, int pageSize, string sortColumn, string sortOrder)
        {
            return await repository.GetAllTeamAsync(pageIndex, pageSize, sortColumn, sortOrder);
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
