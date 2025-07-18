using FluentValidation;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using Race.Shared.Utilities.Paging;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Interfaces.Repositories;
using TeamManagementService.Application.Interfaces.Services;

namespace TeamManagementService.Application.Services;

public class PilotService(IPilotRepository pilotRepository, ILogger<PilotService> logger, IValidator<PilotDeleteDto> pilotValidator) : IPilotService
{
    public async Task<PilotListDto> CreateAsync(PilotCreateDto createDto, CancellationToken token)
    {
        var result = await pilotRepository.CreateAsync(createDto, token);
        return PilotListDto.FromPilot(result);
    }

    public async Task<IPagedList<PilotListDto>> GetAllAsync(PagerParameters pagerParameters, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(pagerParameters, nameof(pagerParameters));

        logger.LogInformation("Fetching all pilots with pagination parameters: {@PagerParameters}", pagerParameters);
        return await pilotRepository.GetAllAsync(pagerParameters, token);
    }

    public async Task<PilotDetailsDto> GetByIdAsync(int id, CancellationToken token)
    {
        logger.LogInformation("Fetching pilot with ID: {Id}", id);

        return await pilotRepository.GetByIdAsync(id, token);
    }

    public async Task UpdateAsync(int id, PilotUpdateDto updateDto, CancellationToken token)
    {
        logger.LogInformation("Updating pilot with ID: {Id}", id);

        await pilotRepository.UpdateAsync(id, updateDto, token);
    }

    public async Task<OneOf<int, NotFound, Error>> DeleteAsync(int id, CancellationToken token)
    {
        var pilotDeleteDto = new PilotDeleteDto(id, string.Empty, string.Empty, string.Empty, string.Empty, 0);        
        await pilotValidator.ValidateAndThrowAsync(pilotDeleteDto, token);

        logger.LogInformation("Attempting to delete pilot with ID: {Id}", id);

        return await pilotRepository.DeleteAsync(id, token);
    }
}