using Race.Repo.Dtos.Teams;
using Race.Repo.Interfaces;
using Race.Service.Interfaces;
using Race.Shared.Paging;
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
            return repository.DeleteAsync(id);
        }

        public async Task<IPagedList<TeamListDto>> GetAllTeamAsync(
            int pageIndex,
            int pageSize,
            string sortColumn = null,
            string sortOrder = null,
            string filterColumn = null,
            string filterQuery = null)
        {
            return await repository.GetAllTeamAsync(
                pageIndex,
                pageSize,
                sortColumn,
                sortOrder,
                filterColumn,
                filterQuery);
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
