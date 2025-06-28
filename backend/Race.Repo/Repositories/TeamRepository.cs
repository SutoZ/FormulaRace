using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Race.Model.Models;
using Race.Repo.ApplicationContext;
using Race.Repo.Dtos.Teams;
using Race.Repo.Interfaces;
using Race.Shared.Paging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Race.Repo.Repositories;

public class TeamRepository(RaceContext context, IMapper mapper, ILogger logger) : ITeamRepository
{
    public async Task<IPagedList<TeamListDto>> GetAllAsync(PagerParameters pagerParameters, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(pagerParameters, nameof(pagerParameters));

        var teams = context.Teams.AsNoTracking();
        var pagedList = await PagedList<TeamListDto>.CreateAsync(teams.Select(ent => TeamListDto.FromTeam(ent)).AsQueryable(), pagerParameters, token);

        logger.Information("Retrieved {Count} teams with pagination parameters: {PagerParameters}", pagerParameters.PageSize, pagerParameters.PageIndex);

        return pagedList;
    }

    public async Task<TeamDetailsDto> GetByIdAsync(int id, CancellationToken token)
    {
        if (id < 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than or equal to 0.");

        var team = await context.Teams
            .Include(ent => ent.Pilots)
            .AsNoTracking()
            .FirstOrDefaultAsync(ent => ent.Id == id, token);

        if (team is null)
            logger.Information("Team with id: {Id} not found.", id);

        return mapper.Map<TeamDetailsDto>(team);
    }

    public async Task<int> DeleteAsync(int id, CancellationToken token)
    {
        if (id < 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than or equal to 0.");

        var team = await context.Teams.FirstOrDefaultAsync(ent => ent.Id == id, token);

        if (team is null)
        {
            logger.Information("Team with id: {Id} not found.", id);
            throw new KeyNotFoundException($"Team with id: {id} not found.");
        }

        context.Teams.Remove(team);

        logger.Information("Team with id: {Id} deleted successfully.", id);

        await context.SaveChangesAsync(token);
        return team.Id;
    }

    public async Task<int> InsertAsync(TeamCreateDto createDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(createDto, nameof(createDto));

        bool nameExists = await context.Teams.AnyAsync(x => x.Name.ToLower() == createDto.Name.ToLower(), token);

        if (nameExists)
        {
            logger.Information("Team with name: {Name} already exists.", createDto.Name);
            throw new InvalidOperationException($"Team with name: {createDto.Name} already exists.");
        }

        Team newTeam = mapper.Map<Team>(createDto);
        context.Teams.Add(newTeam);

        logger.Information("Team with name: {Name} created successfully.", newTeam.Name);

        await context.SaveChangesAsync(token);
        return newTeam.Id;
    }

    public async Task UpdateAsync(int id, TeamUpdateDto updateDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(updateDto, nameof(updateDto));

        var teamExists = await context.Teams.AnyAsync(x => x.Id == id, token);

        if (!teamExists)
        {
            logger.Information("Team with id: {Id} not found.", id);
            throw new KeyNotFoundException($"Team with id: {id} not found.");
        }

        var team = await context.Teams.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, token);


        mapper.Map<TeamUpdateDto, Team>(updateDto);
        await context.SaveChangesAsync(token);

        logger.Information("Team with id: {Id} updated successfully.", id);
    }

    public async Task<int> CreateAsync(TeamCreateDto createDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(createDto, nameof(createDto));

        // Business rule: Pilot name must be unique (case-insensitive)
        bool nameExists = await context.Teams
            .AnyAsync(x => x.Name.ToLower() == createDto.Name.ToLower(), token);

        if (nameExists)
        {
            logger.Information("Team with name: {Name} already exists.", createDto.Name);
            throw new InvalidOperationException($"Team with name: {createDto.Name} already exists.");
        }

        var team = mapper.Map<Team>(createDto);

        context.Teams.Add(team);
        await context.SaveChangesAsync(token);

        logger.Information("Team with name: {Name} created successfully.", createDto.Name);

        return team.Id;
    }
}