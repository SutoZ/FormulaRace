using AutoMapper;
using AutoMapper.QueryableExtensions;
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
using TeamManagementService.Infrastructure.Exceptions;

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
        await context.SaveChangesAsync(token); // ✅ FIX: Save changes to database
        logger.LogInformation("Pilot with id: {Id} deleted successfully.", id);

        return pilot.Id;
    }

    public async Task<OneOf<IPagedList<PilotListDto>, NotFound, Error>> GetAllAsync(PagerParameters pagerParameters,
        Expression<Func<Pilot, bool>> predicate, CancellationToken token)
    {
        var query = context.Pilots
            .Include(x => x.Team)
            .Where(predicate)
            .AsNoTracking();

        logger.LogInformation("Retrieving pilots with pagination parameters: {@PagerParameters}", pagerParameters);

        Expression<Func<Pilot, PilotListDto>> projection = x => new PilotListDto
        {
            Id = x.Id,
            Code = x.Code,
            Name = x.Name,
            Nationality = x.Nationality,
            Number = x.Number,
            TeamListDto = new TeamListDto
            {
                Id = x.Team.Id, ChampionShipPoints = x.Team.ChampionShipPoints,
                DateOfFoundation = x.Team.DateOfFoundation, Name = x.Team.Name, OwnerName = x.Team.OwnerName
            }
        };

        var result = await PagedList<PilotListDto>.CreateAsync(query, pagerParameters, projection, token);

        if (result is null)
            return new NotFound();

        logger.LogInformation("Retrieved {Count} pilots successfully.", result.Count);

        return result;
    }

    public async Task<OneOf<PilotDetailsDto, NotFound, Error>> GetByIdAsync(int id, CancellationToken token)
    {
        var result = await context.Pilots
            .Include(ent => ent.Team)
            .AsNoTracking()
            .ProjectTo<PilotDetailsDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id, token);

        if (result is null)
        {
            logger.LogInformation("Pilot with id: {Id} not found.", id);
            return new NotFound();
        }

        return result;
    }

    public async Task<Pilot> CreateAsync(PilotCreateDto createDto, CancellationToken token)
    {
        bool nameExists = await context.Pilots.AnyAsync(x => x.Name.ToLower() == createDto.Name.ToLower(), token);
        if (nameExists)
        {
            logger.LogInformation("Pilot with name: {Name} already exists.", createDto.Name);
            throw new ArgumentException($"Pilot with name: {createDto.Name} already exists.");
        }

        Pilot pilot = mapper.Map<Pilot>(createDto);

        context.Pilots.Add(pilot);
        await context.SaveChangesAsync(token); // ✅ FIX: Save changes to database
        logger.LogInformation("Pilot with name: {Name} created successfully.", createDto.Name);

        return pilot;
    }

    public async Task UpdateAsync(int id, PilotUpdateDto updateDto, CancellationToken token)
    {
        var pilot = await context.Pilots.FirstOrDefaultAsync(x => x.Id == id, token);

        if (pilot is null)
        {
            logger.LogInformation("Pilot with id: {Id} not found.", id);
            throw new KeyNotFoundException($"Pilot with id: {id} not found.");
        }

        try
        {
            mapper.Map(updateDto, pilot);
            context.Pilots.Update(pilot);
            await context.SaveChangesAsync(token);

            logger.LogInformation("Pilot with id: {Id} updated successfully.", id);
        }
        catch (DbUpdateConcurrencyException)
        {
            logger.LogWarning("Concurrency conflict detected while updating pilot with id: {Id}.", id);
            throw new ConcurrencyException("Pilot", id);
        }
    }
}