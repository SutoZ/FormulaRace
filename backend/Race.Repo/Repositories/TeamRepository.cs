using AutoMapper;
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
        private readonly IMapper mapper;

        public TeamRepository(RaceContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IPagedList<TeamListDto>> GetAllTeamAsync(
            int pageIndex,
            int pageSize,
            string sortColumn = null,
            string sortOrder = null,
            string filterColumn = null,
            string filterQuery = null)
        {
            var teams = context.Teams;
            return await PagedList<TeamListDto>.CreateAsync(teams.Select(ent => new TeamListDto(ent)).AsQueryable(),
                pageIndex,
                pageSize,
                sortColumn,
                sortOrder,
                filterColumn,
                filterQuery);
        }

        public async Task<TeamDetailsDto> GetTeamByIdAsync(int id)
        {
            var team = await context.Teams.Include(ent => ent.Pilots).FirstOrDefaultAsync(ent => ent.Id == id);
            return mapper.Map<TeamDetailsDto>(team);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var team = await context.Teams.FirstOrDefaultAsync(ent => ent.Id == id);
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
