using Microsoft.AspNetCore.Mvc;
using Race.Service.Interfaces;
using System.Threading.Tasks;
using Race.Repo.Dtos.Pilots;
using System.Collections.Generic;
using Swashbuckle.Swagger.Annotations;

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
        public async Task<List<PilotListDto>> GetAllPilot()
        {
            return await pilotService.GetAllPilot();
        }

        [HttpGet("{id}")]
        public async Task<PilotDetailsDto> GetPilot(int id)
        {
            return await pilotService.GetPilotAsync(id);

        }

        [HttpPost]
        [SwaggerOperation(Tags = new[] { OPNAME })]
        public async Task<int> CreatePilot([FromBody] PilotCreateDto createDto)
        {
            return await pilotService.CreatePilotAsync(createDto);
        }

      
        [HttpPut("{id}")]
        [SwaggerOperation(Tags = new[] { OPNAME })]
        public async Task UpdatePilot(int id, [FromBody] PilotUpdateDto updateDto)
        {
            await pilotService.UpdatePilotAsync(id, updateDto);
        }


        //HttpDelete
    }
}
