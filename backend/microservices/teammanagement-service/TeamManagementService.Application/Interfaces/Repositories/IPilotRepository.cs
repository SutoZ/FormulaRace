using Race.Shared.Utilities.Paging;
using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagementService.Application.Interfaces.Repositories;

public interface IPilotRepository
{
    Task<int> CreateAsync(PilotCreateDto entity, CancellationToken token);
    Task<IPagedList<PilotListDto>> GetAllAsync(PagerParameters pagerParameters, CancellationToken token);
    Task<PilotDetailsDto> GetByIdAsync(int id, CancellationToken token);
    Task UpdateAsync(int id, PilotUpdateDto updateDto, CancellationToken token);
    Task<int> DeleteAsync(int id, CancellationToken token);
    Task<int> InsertAsync(PilotCreateDto createDto, CancellationToken token);
}