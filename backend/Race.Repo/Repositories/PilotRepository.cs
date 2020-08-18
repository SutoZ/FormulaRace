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

        public async Task<int> DeleteAsync(int id)
        {
            var pilot = await context.Pilots.FirstOrDefaultAsync(x => x.Id == id);
            if (pilot == null) throw new Exception("Entity not found by given Id");

            context.Remove(pilot);

            await context.SaveChangesAsync();
            return pilot.Id;
        }

        public async Task<List<PilotListDto>> GetAllPilotAsync()
        {
            var pilots = await context.Pilots.AsNoTracking().ToListAsync();
            return (pilots.Select(pilot => new PilotListDto(pilot))).ToList();
        }

        public async Task<PilotDetailsDto> GetPilotAsync(int id)
        {
            var pilot = await context.Pilots.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return new PilotDetailsDto(pilot);
        }

        public async Task<int> InsertAsync(PilotCreateDto createDto)
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
            return createDto.Id;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdatePilotAsync(int id, PilotUpdateDto updateDto)
        {
            var pilot = await context.Pilots.FirstOrDefaultAsync(x => x.Id == id);
            pilot = updateDto.UpdateModelObject(pilot);

            await context.SaveChangesAsync();
        }
    }
}
