using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Race.Shared.Utilities.Paging;
using System.Linq.Expressions;
using TeamManagementService.Application.Dtos.Teams;
using TeamManagementService.Application.Interfaces.Repositories;
using TeamManagementService.Domain.Models;
using TeamManagementService.Infrastructure.ApplicationContext;

namespace TeamManagementService.Infrastructure.Repositories;

public class TeamRepository(RaceContext context, IMapper mapper, ILogger<TeamRepository> logger) : ITeamRepository
{
    public async Task<IPagedList<TeamListDto>> GetAllAsync(PagerParameters pagerParameters, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(pagerParameters, nameof(pagerParameters));

        logger.LogInformation("Retrieving teams with pagination parameters: {@PagerParameters}", pagerParameters);

        var query = context.Teams.AsNoTracking();

        Expression<Func<Team, TeamListDto>> projection = ent => new TeamListDto(
            ent.Id,
            ent.Name,
            ent.DateOfFoundation,
            ent.OwnerName,
            ent.ChampionShipPoints);

        var pagedList = await PagedList<TeamListDto>.CreateAsync(query, pagerParameters, projection, token);

        logger.LogInformation("Retrieved {PageSize} teams on page {PageIndex}", pagerParameters.PageSize, pagerParameters.PageIndex);

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
            logger.LogInformation("Team with id: {Id} not found.", id);

        return mapper.Map<TeamDetailsDto>(team);
    }

    public async Task<int> DeleteAsync(int id, CancellationToken token)
    {
        if (id < 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than or equal to 0.");

        var team = await context.Teams.FirstOrDefaultAsync(ent => ent.Id == id, token);

        if (team is null)
        {
            logger.LogInformation("Team with id: {Id} not found.", id);
            throw new KeyNotFoundException($"Team with id: {id} not found.");
        }

        context.Teams.Remove(team);
        logger.LogInformation("Team with id: {Id} deleted successfully.", id);

        return team.Id;
    }

    public async Task UpdateAsync(int id, TeamUpdateDto updateDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(updateDto, nameof(updateDto));

        var teamExists = await context.Teams.AnyAsync(x => x.Id == id, token);

        if (!teamExists)
        {
            logger.LogInformation("Team with id: {Id} not found.", id);
            throw new KeyNotFoundException($"Team with id: {id} not found.");
        }

        var team = await context.Teams.FirstOrDefaultAsync(x => x.Id == id, token);


        mapper.Map<TeamUpdateDto, Team>(updateDto);
        logger.LogInformation("Team with id: {Id} updated successfully.", id);
    }

    public async Task<int> CreateAsync(TeamCreateDto createDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(createDto, nameof(createDto));

        // Business rule: Pilot name must be unique (case-insensitive)
        bool nameExists = await context.Teams
            .AnyAsync(x => x.Name.ToLower() == createDto.Name.ToLower(), token);

        if (nameExists)
        {
            logger.LogInformation("Team with name: {Name} already exists.", createDto.Name);
            throw new InvalidOperationException($"Team with name: {createDto.Name} already exists.");
        }

        var team = mapper.Map<Team>(createDto);

        context.Teams.Add(team);
        logger.LogInformation("Team with name: {Name} created successfully.", createDto.Name);

        return team.Id;
    }
}