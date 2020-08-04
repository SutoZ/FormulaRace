using Microsoft.EntityFrameworkCore;
using Race.Repo.ApplicationContext;
using Race.Repo.Dtos.Pilots;
using Race.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Race.Repo.Repositories
{
    public class PilotRepository : IPilotRepository
    {
        private readonly RaceContext context;

        public PilotRepository(RaceContext context)
        {
            this.context = context;
        }    

        public async Task<Guid> DeleteAsync(Guid id)
        {
            var pilot = await context.Pilots.FirstOrDefaultAsync(x => x.PilotId == id);
            if (pilot == null) throw new Exception("Entity not found by given Id");

            context.Remove(pilot);

            await context.SaveChangesAsync();
            return pilot.PilotId;
        }

        public async Task<List<PilotListDto>> GetAllPilotAsync()
        {
            var pilots = await context.Pilots.AsNoTracking().ToListAsync();
            return (pilots.Select(pilot => new PilotListDto(pilot))).ToList();
        }

        public async Task<PilotDetailsDto> GetPilotAsync(Guid id)
        {
            var pilot = await context.Pilots.AsNoTracking().FirstOrDefaultAsync(x => x.PilotId == id);
            return new PilotDetailsDto(pilot);
        }

        public async Task<Guid> InsertAsync(PilotCreateDto createDto)
        {
            try
            {
                if (createDto == null) throw new ArgumentNullException("entity");
                var pilot = createDto.CreateModelObject();

                context.Add(pilot);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            return createDto.PilotId;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdatePilotAsync(Guid id, PilotUpdateDto updateDto)
        {
            var pilot = await context.Pilots.FirstOrDefaultAsync(x => x.PilotId == id);
            pilot = updateDto.UpdateModelObject(pilot);

            await context.SaveChangesAsync();
        }
    }
}
