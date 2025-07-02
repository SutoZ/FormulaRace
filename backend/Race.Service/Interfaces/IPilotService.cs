using Race.Repo.Dtos.Pilots;
using Race.Shared.Paging;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Service.Interfaces;

public interface IPilotService
{
    Task<IPagedList<PilotListDto>> GetAllAsync(PagerParameters pagerParameters, CancellationToken token);
    Task<PilotDetailsDto> GetByIdAsync(int id, CancellationToken token);
    Task<int> CreateAsync(PilotCreateDto createDto, CancellationToken token);
    Task UpdateAsync(int id, PilotUpdateDto updateDto, CancellationToken token);
    Task<int> DeleteAsync(int id, CancellationToken token);
}