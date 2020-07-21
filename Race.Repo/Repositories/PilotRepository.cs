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
        public Task CreateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            //var pilot = await context.FirstOrDefaultAsync(x => x.Id == id);
            //if (pilot == null) throw new Exception("Entity not found by given Id");

            await context.SaveChangesAsync();
        }

        public async Task<List<PilotListDto>> GetAllPilotAsync()
        {
            var pilots = await context.Pilots.ToListAsync();
            return (pilots.Select(pilot => new PilotListDto(pilot))).ToList();
        }

        public async Task<PilotDetailsDto> GetPilotAsync(int id)
        {
            var pilot = await context.Pilots.FirstOrDefaultAsync(x => x.PilotId == id);
            return new PilotDetailsDto(pilot);
        }      

        public async Task<int> InsertAsync(PilotCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException("entity");

            context.Add(createDto);
            await context.SaveChangesAsync();
            return createDto.Id;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdatePilotAsync(int id, PilotUpdateDto updateDto)
        {
            var pilot = await context.Pilots.FirstOrDefaultAsync(x => x.PilotId == id);
            pilot = updateDto.UpdateModelObject(pilot);

            await context.SaveChangesAsync();
        }     
    }
}
