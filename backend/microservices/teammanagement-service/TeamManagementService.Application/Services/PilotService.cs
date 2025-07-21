using FluentValidation;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using Race.Shared.Utilities.Paging;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Interfaces.Repositories;
using TeamManagementService.Application.Interfaces.Services;
using TeamManagementService.Application.Specifications;

namespace TeamManagementService.Application.Services;

public class PilotService(
    IPilotRepository pilotRepository,
    ILogger<PilotService> logger,
    IValidator<PilotDeleteDto> deleteValidator,
    IValidator<PilotFilterDto> getByIdValidator) : IPilotService
{
    public async Task<PilotListDto> CreateAsync(PilotCreateDto createDto, CancellationToken token)
    {
        var result = await pilotRepository.CreateAsync(createDto, token);
        return PilotListDto.FromPilot(result);
    }

    public async Task<OneOf<IPagedList<PilotListDto>, NotFound, Error>> GetAllAsync(PagerParameters pagerParameters, PilotFilterDto filterDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(pagerParameters, nameof(pagerParameters));
        await getByIdValidator.ValidateAndThrowAsync(filterDto, token);

        var query = PilotFilterSpecifiation.AsExpression(filterDto);

        logger.LogInformation("Fetching all pilots with pagination parameters: {@PagerParameters}", pagerParameters);
        return await pilotRepository.GetAllAsync(pagerParameters, query, token);
    }

    public async Task<OneOf<PilotDetailsDto, NotFound, Error>> GetByIdAsync(int id, CancellationToken token)
    {
        logger.LogInformation("Fetching pilot with ID: {Id}", id);

        PilotFilterDto filterDto = new(id, string.Empty, string.Empty, string.Empty, string.Empty);

        await getByIdValidator.ValidateAndThrowAsync(filterDto, token);

        var result = await pilotRepository.GetByIdAsync(id, token);
        logger.LogInformation("Pilot retrieval result for ID {Id}: {@Result}", id, result);

        return result;
    }

    public async Task UpdateAsync(int id, PilotUpdateDto updateDto, CancellationToken token)
    {
        logger.LogInformation("Updating pilot with ID: {Id}", id);

        await pilotRepository.UpdateAsync(id, updateDto, token);
    }

    public async Task<OneOf<int, NotFound, Error>> DeleteAsync(int id, CancellationToken token)
    {
        logger.LogInformation("Attempting to delete pilot with ID: {Id}", id);

        var pilotDeleteDto = new PilotDeleteDto(id, string.Empty, string.Empty, string.Empty, string.Empty, 0);
        await deleteValidator.ValidateAndThrowAsync(pilotDeleteDto, token);

        var result = await pilotRepository.DeleteAsync(id, token);
        logger.LogInformation("Response for delete request for pilot ID {Id}: {@Result}", id, result);

        return result;
    }
}