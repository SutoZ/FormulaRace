using Race.Repo.Dtos.Pilots;
using Race.Repo.Interfaces;
using Race.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Race.Service.Services
{
    public class PilotService : IPilotService
    {
        private readonly IPilotRepository pilotRepository;

        public PilotService(IPilotRepository pilotRepository)
        {
            this.pilotRepository = pilotRepository;
        }

        public async Task<Guid> CreatePilotAsync(PilotCreateDto createDto)
        {
            return await pilotRepository.InsertAsync(createDto);
        }

        public async Task<List<PilotListDto>> GetAllPilotAsync()
        {
            return await pilotRepository.GetAllPilotAsync();
        }

        public async Task<PilotDetailsDto> GetPilotAsync(Guid id)
        {
           return await pilotRepository.GetPilotAsync(id);
        }
     
        public async Task UpdatePilotAsync(Guid id, PilotUpdateDto updateDto)
        {
            await pilotRepository.UpdatePilotAsync(id, updateDto);
        }
        public async Task<Guid> DeletePilotAsync(Guid id)
        {
           return await pilotRepository.DeleteAsync(id);
        }     
    }
}
