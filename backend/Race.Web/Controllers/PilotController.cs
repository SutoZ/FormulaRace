using Microsoft.AspNetCore.Mvc;
using Race.Service.Interfaces;
using System.Threading.Tasks;
using Race.Repo.Dtos.Pilots;
using System.Collections.Generic;
using Swashbuckle.Swagger.Annotations;
using System;

namespace Race.Web.Controllers
{
    [Route("api/pilots")]
    [ApiController]
    public class PilotController : Controller
    {
        private const string OPNAME = "Pilots";

        private readonly IPilotService pilotService;

        public PilotController(IPilotService pilotService)
        {
            this.pilotService = pilotService;
        }

        [HttpGet]
        [SwaggerOperation(Tags = new[] { OPNAME })]
        public async Task<ActionResult<List<PilotListDto>>> GetAllPilot()
        {
            return await pilotService.GetAllPilotAsync();
        }

        [HttpGet("{id}")]
        public async Task<PilotDetailsDto> GetPilot(Guid id)
        {
            return await pilotService.GetPilotAsync(id);

        }

        [HttpPost]
        [SwaggerOperation(Tags = new[] { OPNAME })]
        public async Task<Guid> CreatePilot([FromBody] PilotCreateDto createDto)        //Task<ActionResult<PilotCreateDto>>
        {
            return await pilotService.CreatePilotAsync(createDto);
        }


        [HttpPut("{id}")]
        [SwaggerOperation(Tags = new[] { OPNAME })]
        public async Task UpdatePilot(Guid id, [FromBody] PilotUpdateDto updateDto)
        {
            await pilotService.UpdatePilotAsync(id, updateDto);
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Tags = new[] { OPNAME })]
        public async Task<Guid> DeletePilot(Guid id)
        {
          return await pilotService.DeletePilotAsync(id);
        }

    }
}
