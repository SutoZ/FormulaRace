using Microsoft.AspNetCore.Mvc;
using Race.Repo.Dtos.Teams;
using Race.Repo.Interfaces;
using Race.Service.Interfaces;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Race.Web.Controllers
{
    [ApiController]
    public class TeamController
    {
        
        private const string OPNAME = "Teams";

        private readonly ITeamService teamService;

        public TeamController(ITeamService service)
        {
            this.teamService = service;
        }

        [HttpGet]
        [SwaggerOperation(Tags = new string[] { OPNAME })]
        public ActionResult<Task<List<TeamListDto>>> GetAllTeams()
        {
            return teamService.GetAllTeamAsync();
        }

        [HttpGet("{id}")]
        public async Task<TeamDetailsDto> GetTeam(int id)
        {
            return await teamService.GetTeamByIdAsync(id);

        }

        [HttpPost]
        [SwaggerOperation(Tags = new[] { OPNAME })]
        public async Task<ActionResult<int>> CreateTeam([FromBody] TeamCreateDto createDto)
        {
            return await teamService.CreateTeamAsync(createDto);
        }


        [HttpPut("{id}")]
        [SwaggerOperation(Tags = new[] { OPNAME })]
        public async Task UpdateTeam(int id, [FromBody] TeamUpdateDto updateDto)
        {
            await teamService.UpdateTeamAsync(id, updateDto);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Tags = new[] { OPNAME })]
        public async Task<int> DeleteTeam(int id)
        {
            return await teamService.DeleteTeamAsync(id);
        }
    }
}
