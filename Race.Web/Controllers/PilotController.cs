using Microsoft.AspNetCore.Mvc;
using Race.Service.Interfaces;
using System.Threading.Tasks;
using Race.Repo.Dtos.Pilots;
using System.Collections.Generic;

namespace Race.Web.Controllers
{
    [Route("api/pilots")]
    [ApiController]
    public class PilotController : Controller
    {
        private readonly IPilotService pilotService;

        public PilotController(IPilotService pilotService)
        {
            this.pilotService = pilotService;
        }

        public async Task<int> InsertPilot(PilotCreateDto createDto)
        {
            return await pilotService.InsertPilotAsync(createDto);
        }

        [HttpGet]
        public async Task<List<PilotListDto>> GetAllPilot()
        {
            return await pilotService.GetAllPilot();
        }

        [HttpGet("{id}")]
        public async Task<PilotDetailsDto> GetPilot([FromBody]int id)
        {
            return await pilotService.GetPilotAsync(id);
            
        }

        public async Task UpdatePilot(int id, PilotUpdateDto updateDto)
        {
            await pilotService.UpdatePilotAsync(id, updateDto);
        }

    }
}
