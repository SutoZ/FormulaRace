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

        public async Task<int> CreatePilotAsync(PilotCreateDto createDto)
        {
            return await pilotRepository.InsertAsync(createDto);
        }

        public async Task<List<PilotListDto>> GetAllPilotAsync()
        {
            return await pilotRepository.GetAllPilotAsync();
        }

        public async Task<PilotDetailsDto> GetPilotAsync(int id)
        {
           return await pilotRepository.GetPilotAsync(id);
        }
     
        public async Task UpdatePilotAsync(int id, PilotUpdateDto updateDto)
        {
            await pilotRepository.UpdatePilotAsync(id, updateDto);
        }
        public async Task<int> DeletePilotAsync(int id)
        {
           return await pilotRepository.DeleteAsync(id);
        }     
    }
}
