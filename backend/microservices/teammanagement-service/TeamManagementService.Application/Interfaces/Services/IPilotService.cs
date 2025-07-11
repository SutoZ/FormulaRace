using Race.Shared.Utilities.Paging;
using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagementService.Application.Interfaces.Services;

public interface IPilotService
{
    Task<IPagedList<PilotListDto>> GetAllAsync(PagerParameters pagerParameters, CancellationToken token);
    Task<PilotDetailsDto> GetByIdAsync(int id, CancellationToken token);
    Task<int> CreateAsync(PilotCreateDto createDto, CancellationToken token);
    Task UpdateAsync(int id, PilotUpdateDto updateDto, CancellationToken token);
    Task<int> DeleteAsync(int id, CancellationToken token);
}