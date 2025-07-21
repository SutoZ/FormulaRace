using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using Race.Shared.Utilities.Paging;
using System.Linq.Expressions;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Dtos.Teams;
using TeamManagementService.Application.Interfaces.Repositories;
using TeamManagementService.Domain.Models;
using TeamManagementService.Infrastructure.ApplicationContext;

namespace TeamManagementService.Infrastructure.Repositories;

public class PilotRepository(RaceContext context, IMapper mapper, ILogger<PilotRepository> logger) : IPilotRepository
{
    public async Task<OneOf<int, NotFound, Error>> DeleteAsync(int id, CancellationToken token)
    {
        var pilot = await context.Pilots.FirstOrDefaultAsync(x => x.Id == id, token);

        if (pilot is null)
        {
            logger.LogInformation("Pilot with id: {Id} not found.", id);
            return new NotFound();
        }

        context.Pilots.Remove(pilot);

        await context.SaveChangesAsync(token);
        logger.LogInformation("Pilot with id: {Id} deleted successfully.", id);

        return pilot.Id;
    }

    public async Task<OneOf<IPagedList<PilotListDto>, NotFound, Error>> GetAllAsync(PagerParameters pagerParameters, Expression<Func<Pilot, bool>> predicate, CancellationToken token)
    {
        var query = context.Pilots
            .Include(x => x.Team)
            .Where(predicate)
            .AsNoTracking();

        logger.LogInformation("Retrieving pilots with pagination parameters: {@PagerParameters}", pagerParameters);

        Expression<Func<Pilot, PilotListDto>> projection = x => new PilotListDto(
            x.Id, x.Name, x.Number, x.Code, x.Nationality, x.Team == null ? null : new TeamListDto(
                x.Team.Id, x.Team.Name, x.Team.DateOfFoundation, x.Team.OwnerName, x.Team.ChampionShipPoints));

        var result = await PagedList<PilotListDto>.CreateAsync(query, pagerParameters, projection, token);

        if (result is null)
            return new NotFound();

        logger.LogInformation("Retrieved {Count} pilots successfully.", result.Count);

        return result;
    }

    public async Task<OneOf<PilotDetailsDto, NotFound, Error>> GetByIdAsync(int id, CancellationToken token)
    {
        var pilot = await context.Pilots
            .Include(ent => ent.Team)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, token);

        if (pilot is null)
        {
            logger.LogInformation("Pilot with id: {Id} not found.", id);
            return new NotFound();
        }

        var result = mapper.Map<PilotDetailsDto>(pilot);
        return result;
    }

    public async Task<Pilot> CreateAsync(PilotCreateDto createDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(createDto, nameof(createDto));

        bool nameExists = await context.Pilots.AnyAsync(x => x.Name.ToLower() == createDto.Name.ToLower(), token);
        if (nameExists)
        {
            logger.LogInformation("Pilot with name: {Name} already exists.", createDto.Name);
            throw new ArgumentException($"Pilot with name: {createDto.Name} already exists.");
        }

        Pilot pilot = mapper.Map<Pilot>(createDto);

        context.Pilots.Add(pilot);
        await context.SaveChangesAsync(token);

        logger.LogInformation("Pilot with name: {Name} created successfully.", createDto.Name);

        return pilot;
    }

    public async Task UpdateAsync(int id, PilotUpdateDto updateDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(updateDto, nameof(updateDto));

        var pilotExists = await context.Pilots.AnyAsync(x => x.Id == id, token);

        if (!pilotExists)
        {
            logger.LogInformation("Pilot with id: {Id} not found.", id);
            //return new NotFound();
            throw new KeyNotFoundException($"Pilot with id: {id} not found.");
        }

        var pilot = await context.Pilots.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, token);

        mapper.Map<PilotUpdateDto, Pilot>(updateDto);
        await context.SaveChangesAsync(token);

        logger.LogInformation("Pilot with id: {Id} updated successfully.", id);
    }

    public async Task<int> InsertAsync(PilotCreateDto createDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(createDto, nameof(createDto));

        bool nameExists = await context.Pilots.AnyAsync(x => x.Name.ToLower() == createDto.Name.ToLower(), token);

        if (nameExists)
        {
            logger.LogInformation("Pilot with name: {Name} already exists.", createDto.Name);
            throw new ArgumentException($"Pilot with name: {createDto.Name} already exists.");
        }

        Pilot pilot = mapper.Map<Pilot>(createDto);
        context.Pilots.Add(pilot);

        await context.SaveChangesAsync(token);
        logger.LogInformation("Pilot with name: {Name} created successfully.", createDto.Name);

        return pilot.Id;
    }
}