using Race.Repo.Dtos.Pilots;
using Race.Repo.Interfaces;
using Race.Service.Interfaces;
using Race.Shared.Paging;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Service.Services;

public class PilotService(IPilotRepository pilotRepository) : IPilotService
{
    public async Task<int> CreateAsync(PilotCreateDto createDto, CancellationToken token)
    {
        var result = await pilotRepository.CreateAsync(createDto, token);
        return result;
    }

    public async Task<IPagedList<PilotListDto>> GetAllAsync(PagerParameters pagerParameters, CancellationToken token)
    {
        return await pilotRepository.GetAllAsync(pagerParameters, token);
    }

    public async Task<PilotDetailsDto> GetByIdAsync(int id, CancellationToken token)
    {
        return await pilotRepository.GetByIdAsync(id, token);
    }

    public async Task UpdateAsync(int id, PilotUpdateDto updateDto, CancellationToken token)
    {
        await pilotRepository.UpdateAsync(id, updateDto, token);
    }
    public async Task<int> DeleteAsync(int id, CancellationToken token)
    {
        return await pilotRepository.DeleteAsync(id, token);
    }
}