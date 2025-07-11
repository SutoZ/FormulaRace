﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Race.Model.Models;
using Race.Repo.ApplicationContext;
using Race.Repo.Dtos.Pilots;
using Race.Repo.Dtos.Teams;
using Race.Repo.Interfaces;
using Race.Shared.Paging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Repo.Repositories;

public class PilotRepository(RaceContext context, IMapper mapper, ILogger logger) : IPilotRepository
{
    public async Task<int> DeleteAsync(int id, CancellationToken token)
    {
        if (id < 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than or equal to 0.");

        var pilot = await context.Pilots.FirstOrDefaultAsync(x => x.Id == id, token);

        if (pilot is null)
        {
            logger.Information("Pilot with id: {Id} not found.", id);
            throw new KeyNotFoundException($"Pilot with id: {id} not found.");
        }

        context.Pilots.Remove(pilot);

        logger.Information("Pilot with id: {Id} deleted successfully.", id);

        await context.SaveChangesAsync(token);
        return pilot.Id;
    }

    public async Task<IPagedList<PilotListDto>> GetAllAsync(PagerParameters pagerParameters, CancellationToken token)
    {
        var query = context
            .Pilots
            .Include(x => x.Team)
            .AsNoTracking();

        logger.Information("Retrieving pilots with pagination parameters: {@PagerParameters}", pagerParameters);

        Expression<Func<Pilot, PilotListDto>> projection = x => new PilotListDto(
            x.Id, x.Name, x.Number, x.Code, x.Nationality, x.Team == null ? null : new TeamListDto(
                x.Team.Id, x.Team.Name, x.Team.DateOfFoundation, x.Team.OwnerName, x.Team.ChampionShipPoints));

        return await PagedList<PilotListDto>.CreateAsync(query, pagerParameters, projection, token);
    }

    public async Task<PilotDetailsDto> GetByIdAsync(int id, CancellationToken token)
    {
        if (id < 0)
        {
            logger.Information("Id must be greater than or equal to 0. Provided id: {Id}", id);
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than or equal to 0.");
        }

        var pilot = await context.Pilots
            .Include(ent => ent.Team)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, token);

        if (pilot is null)
            logger.Information("Pilot with id: {Id} not found.", id);

        return mapper.Map<PilotDetailsDto>(pilot);
    }

    public async Task<int> CreateAsync(PilotCreateDto createDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(createDto, nameof(createDto));

        bool nameExists = await context.Pilots.AnyAsync(x => x.Name.ToLower() == createDto.Name.ToLower(), token);
        if (nameExists)
        {
            logger.Information("Pilot with name: {Name} already exists.", createDto.Name);
            throw new ArgumentException($"Pilot with name: {createDto.Name} already exists.");
        }

        Pilot pilot = mapper.Map<Pilot>(createDto);

        context.Pilots.Add(pilot);
        await context.SaveChangesAsync(token);

        logger.Information("Pilot with name: {Name} created successfully.", createDto.Name);

        return pilot.Id;
    }

    public async Task UpdateAsync(int id, PilotUpdateDto updateDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(updateDto, nameof(updateDto));

        var pilotExists = await context.Pilots.AnyAsync(x => x.Id == id, token);

        if (!pilotExists)
        {
            logger.Information("Pilot with id: {Id} not found.", id);
            throw new KeyNotFoundException($"Pilot with id: {id} not found.");
        }

        var pilot = await context.Pilots.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, token);

        mapper.Map<PilotUpdateDto, Pilot>(updateDto);
        await context.SaveChangesAsync(token);

        logger.Information("Pilot with id: {Id} updated successfully.", id);
    }

    public async Task<int> InsertAsync(PilotCreateDto createDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(createDto, nameof(createDto));

        bool nameExists = await context.Pilots.AnyAsync(x => x.Name.ToLower() == createDto.Name.ToLower(), token);

        if (nameExists)
        {
            logger.Information("Pilot with name: {Name} already exists.", createDto.Name);
            throw new ArgumentException($"Pilot with name: {createDto.Name} already exists.");
        }

        Pilot pilot = mapper.Map<Pilot>(createDto);
        context.Pilots.Add(pilot);

        await context.SaveChangesAsync(token);
        logger.Information("Pilot with name: {Name} created successfully.", createDto.Name);

        return pilot.Id;
    }
}