﻿using Race.Repo.Dtos.Teams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Race.Repo.Interfaces
{
    public interface ITeamRepository
    {
        Task<int> InsertAsync(TeamCreateDto entity);
        Task<List<TeamListDto>> GetAllTeamAsync();
        Task<TeamDetailsDto> GetTeamByIdAsync(int id);
        Task UpdateTeamAsync(int id, TeamUpdateDto updateDto);
        Task<int> DeleteAsync(int id);
    }
}