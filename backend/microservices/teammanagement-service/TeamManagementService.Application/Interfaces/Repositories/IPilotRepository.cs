using OneOf;
using OneOf.Types;
using Race.Shared.Utilities.Paging;
using System.Linq.Expressions;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Interfaces.Repositories;

public interface IPilotRepository
{
    Task<Pilot> CreateAsync(PilotCreateDto entity, CancellationToken token);
    Task<OneOf<IPagedList<PilotListDto>, NotFound, Error>> GetAllAsync(PagerParameters pagerParameters, Expression<Func<Pilot, bool>> predicate, CancellationToken token);
    Task<OneOf<PilotDetailsDto, NotFound, Error>> GetByIdAsync(int id, CancellationToken token);
    Task UpdateAsync(int id, PilotUpdateDto updateDto, CancellationToken token);
    Task<OneOf<int, NotFound, Error>> DeleteAsync(int id, CancellationToken token);
    Task<int> InsertAsync(PilotCreateDto createDto, CancellationToken token);
}