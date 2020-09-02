using Race.Repo.Dtos;
using Race.Repo.Dtos.Pilots;
using Race.Repo.Interfaces;
using Race.Service.Interfaces;
using Race.Shared.Paging;
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

        public async Task<int> CreatePilotAsync(int id, PilotCreateDto createDto)
        {
            return await pilotRepository.InsertAsync(id, createDto);
        }

        public async Task<IPagedList<PilotListDto>> GetAllPilotAsync(
            int pageIndex,
            int pageSize,
            string sortColumn = null,
            string sortOrder = null,
            string filterColumn = null,
            string filterQuery = null)
        {
            return await pilotRepository.GetAllPilotAsync(pageIndex, pageSize, sortColumn, sortOrder, filterColumn, filterQuery);
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
