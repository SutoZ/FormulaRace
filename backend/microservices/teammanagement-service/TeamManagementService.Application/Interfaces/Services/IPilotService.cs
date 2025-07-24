using OneOf;
using OneOf.Types;
using Race.Shared.Utilities.Paging;
using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagementService.Application.Interfaces.Services;

public interface IPilotService
{
    Task<OneOf<IPagedList<PilotListDto>, NotFound, Error>> GetAllAsync(PagerParameters pagerParameters, PilotFilterDto filterDto, CancellationToken token);
    Task<OneOf<PilotDetailsDto, NotFound, Error>> GetByIdAsync(int id, CancellationToken token);
    Task<PilotListDto> CreateAsync(PilotCreateDto createDto, CancellationToken token);
    Task UpdateAsync(int id, PilotUpdateDto updateDto, CancellationToken token);
    Task<OneOf<int, NotFound, Error>> DeleteAsync(int id, CancellationToken token);
}