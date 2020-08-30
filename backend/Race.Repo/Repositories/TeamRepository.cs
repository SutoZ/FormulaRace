using Microsoft.EntityFrameworkCore;
using Race.Repo.ApplicationContext;
using Race.Repo.Dtos.Teams;
using Race.Repo.Interfaces;
using Race.Shared.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Race.Repo.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly RaceContext context;

        public TeamRepository(RaceContext context)
        {
            this.context = context;
        }

        public async Task<PagedList<TeamListDto>> GetAllTeamAsync(int pageIndex, int pageSize, string sortColumn, string sortOrder)
        {
            var teams = await context.Teams.Include(ent => ent.Pilots).ToListAsync();
            return PagedList<TeamListDto>.Create(teams.Select(ent => new TeamListDto(ent)).AsQueryable(), pageIndex, pageSize, sortColumn, sortOrder);
        }

        public async Task<TeamDetailsDto> GetTeamByIdAsync(int id)
        {
            var team = await context.Teams.Include(ent => ent.Pilots).FirstOrDefaultAsync(ent => ent.Id == id);
            return new TeamDetailsDto(team);
        }

        public async Task<int> DeleteAsync(int id)
        {
             var team = await context.Teams.FirstAsync(ent => ent.Id == id);
            if (team == null) throw new Exception("Entity not found by given Id");

            context.Teams.Remove(team);

            await context.SaveChangesAsync();
            return team.Id;
        }

        public async Task<int> InsertAsync(TeamCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException("Entity was null");

            try
            {
                var newTeam = createDto.CreateModelObject();
                context.Teams.Add(newTeam);

                await context.SaveChangesAsync();
                return newTeam.Id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateTeamAsync(int id, TeamUpdateDto updateDto)
        {
            var team = await context.Teams.FirstOrDefaultAsync(x => x.Id == id);
            team = updateDto.UpdateModelObject(team);

            await context.SaveChangesAsync();
        }
    }
}
