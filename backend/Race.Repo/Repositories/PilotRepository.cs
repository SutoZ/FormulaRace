﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public PilotRepository(RaceContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
            //var pilots = await context.Pilots.Include(x => x.Team).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

        //    PilotListDto pilotListDto = mapper.Map<PilotListDto>(pilots);

            return  await PagedList<PilotListDto>
                .CreateAsync(pilots.Select(ent => new PilotListDto(ent)).AsQueryable(), pageIndex, pageSize, sortColumn, sortOrder, filterColumn, filterQuery);
        }

        public async Task<PilotDetailsDto> GetPilotAsync(int id)
        {
            var pilot = await context.Pilots.Include(ent => ent.Team).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return new PilotDetailsDto(pilot);
        }

        public async Task CreateAsync(PilotCreateDto createDto)
        {
            try
            {
                if (createDto == null) throw new ArgumentNullException("Entity was null");
                var pilot = createDto.CreateModelObject();

                var team = await context.Teams.Include(ent => ent.Pilots).FirstOrDefaultAsync(ent => ent.Id == 2);

                pilot.Team = team;
                pilot.TeamId = team.Id;

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
    }
}
