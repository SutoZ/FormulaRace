using Microsoft.AspNetCore.Mvc;
using Race.Repo.Dtos.Teams;
using Race.Service.Interfaces;
using Race.Shared.Paging;
using Swashbuckle.Swagger.Annotations;
using System.Threading.Tasks;

namespace Race.Web.Controllers
{
    [ApiController]
    [Route("api/teams")]
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
        public Task<PagedList<TeamListDto>> GetAllTeams(
            int pageIndex, 
            int pageSize, 
            string sortColumn = null,
            string sortOrder = null, 
            string filterColumn = null, 
            string filterQuery = null)
        {
            return teamService.GetAllTeamAsync(pageIndex, pageSize, sortColumn, sortOrder, filterColumn, filterQuery);
        }

        [HttpGet("{id}")]
        public Task<TeamDetailsDto> GetTeam(int id)
        {
            return teamService.GetTeamByIdAsync(id);
        }

        [HttpPost]
        [SwaggerOperation(Tags = new[] { OPNAME })]
        public Task<int> CreateTeam([FromBody] TeamCreateDto createDto)
        {
            return teamService.CreateTeamAsync(createDto);
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
