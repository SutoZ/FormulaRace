﻿using Microsoft.AspNetCore.Mvc;
using Race.Service.Interfaces;
using System.Threading.Tasks;
using Race.Repo.Dtos.Pilots;
using Swashbuckle.Swagger.Annotations;
using Race.Shared.Paging;

namespace Race.Web.Controllers
{
    [Route("api/pilots")]
    [ApiController]
    public class PilotController // : ControllerBase
    {
        private const string OPNAME = "Pilots";
        private const int PAGESIZE = 10;

        private readonly IPilotService pilotService;

        public PilotController(IPilotService pilotService)
        {
            this.pilotService = pilotService;
        }

        [HttpGet]
        [SwaggerOperation(Tags = new[] { OPNAME })]
        public async Task<IPagedList<PilotListDto>> GetAllPilot(
            int pageIndex,
            int pageSize = PAGESIZE,
            string sortColumn = null,
            string sortOrder = null,
            string filterColumn = null,
            string filterQuery = null)
        {
            return await pilotService.GetAllPilotAsync(pageIndex, pageSize, sortColumn, sortOrder, filterColumn, filterQuery);
        }

        [HttpGet("{id}")]
        public async Task<PilotDetailsDto> GetPilot(int id)
        {
            return await pilotService.GetPilotAsync(id);
        }

        [HttpPost]
        [SwaggerOperation(Tags = new[] { OPNAME })]
        public async Task CreatePilot([FromBody] PilotCreateDto createDto)
        {
            await pilotService.CreatePilotAsync(createDto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Tags = new[] { OPNAME })]
        public async Task UpdatePilot(int id, [FromQuery] PilotUpdateDto updateDto)
        {
            await pilotService.UpdatePilotAsync(id, updateDto);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Tags = new[] { OPNAME })]
        public async Task<int> DeletePilot(int id)
        {
            return await pilotService.DeletePilotAsync(id);
        }
    }
}
