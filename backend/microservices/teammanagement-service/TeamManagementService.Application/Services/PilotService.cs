using Race.Shared.Utilities.Paging;
using Serilog;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Interfaces.Repositories;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.Services;

public class PilotService(IPilotRepository pilotRepository, ILogger logger) : IPilotService
{
    public async Task<PilotListDto> CreateAsync(PilotCreateDto createDto, CancellationToken token)
    {
        var result = await pilotRepository.CreateAsync(createDto, token);
        return PilotListDto.FromPilot(result);
    }

    public async Task<IPagedList<PilotListDto>> GetAllAsync(PagerParameters pagerParameters, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(pagerParameters, nameof(pagerParameters));

        logger.Information("Fetching all pilots with pagination parameters: {@PagerParameters}", pagerParameters);
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