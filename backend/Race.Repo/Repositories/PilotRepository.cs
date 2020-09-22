using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Race.Model.Models;
using Race.Repo.ApplicationContext;
using Race.Repo.Dtos.Pilots;
using Race.Repo.Interfaces;
using Race.Shared.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Race.Repo.Repositories
{
    public class PilotRepository : IPilotRepository
    {
        private readonly RaceContext context;
        private readonly IMapper mapper;
        private readonly ILogger<PilotRepository> logger;

        public PilotRepository(
            RaceContext context, IMapper mapper,
            ILogger<PilotRepository> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var pilot = await context.Pilots.FirstAsync(x => x.Id == id);
            if (pilot == null) throw new Exception("Entity not found by given Id");

            context.Pilots.Remove(pilot);

            await context.SaveChangesAsync();
            return pilot.Id;
        }

        public async Task<IPagedList<PilotListDto>> GetAllPilotAsync(
            int pageIndex,
            int pageSize,
            string sortColumn = null,
            string sortOrder = null,
            string filterColumn = null,
            string filterQuery = "")
        {
            var pilots = context.Pilots.Include(x => x.Team);

            return await PagedList<PilotListDto>
                    .CreateAsync(pilots.Select(ent => new PilotListDto(ent)).AsQueryable(),
                    pageIndex, pageSize, sortColumn, sortOrder, filterColumn, filterQuery);
        }

        public async Task<PilotDetailsDto> GetPilotAsync(int id)
        {
            Pilot pilot = new Pilot();

            try
            {
                pilot = await context.Pilots.Include(ent => ent.Team).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return mapper.Map<PilotDetailsDto>(pilot);
        }

        public async Task CreateAsync(PilotCreateDto createDto)
        {
            try
            {
                if (createDto == null) throw new ArgumentNullException("Entity was null");
                var pilot = createDto.CreateModelObject();

                context.Pilots.Add(pilot);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public async Task UpdatePilotAsync(int id, PilotUpdateDto updateDto)
        {
            var pilot = await context.Pilots.FirstOrDefaultAsync(x => x.Id == id);
            if (pilot == null) throw new Exception($"Pilot with id: {id} not found.");

            pilot = updateDto.UpdateModelObject(pilot);

            await context.SaveChangesAsync();
        }

        public bool CheckNameExists(PilotDetailsDto pilotDto)
        {
            return context.Pilots.Any(p => p.Name.ToUpper().Trim() == pilotDto.Name.ToUpper().Trim());
        }
    }
}
