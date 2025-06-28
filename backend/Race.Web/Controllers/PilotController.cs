using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Race.Repo.Dtos.Pilots;
using Swashbuckle.Swagger.Annotations;
using Race.Shared.Paging;
using System.Threading;
using MediatR;
using Race.Web.CQRS.Pilots.Queries;
using Race.Web.CQRS.Pilots.Commands;

namespace Race.Web.Controllers;

[ApiController]
[Route("api/pilots")]
public class PilotController(IMediator mediator) : ControllerBase
{
    private const string OPNAME = "Pilots";

    [HttpGet]
    [SwaggerOperation(Tags = new[] { OPNAME })]
    [SwaggerResponse(200, "Returns a paginated list of pilots.")]
    [SwaggerResponse(400, "Invalid pagination parameters.")]
    public async Task<ActionResult<PagedList<PilotListDto>>> GetAll([FromQuery] PagerParameters pagerParameters, CancellationToken token)
    {
        var result = await mediator.Send(new GetAllPilotsQuery(pagerParameters), token);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Tags = new[] { OPNAME })]
    [SwaggerResponse(200, "Returns the pilot details.")]
    [SwaggerResponse(404, "Pilot not found.")]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> GetById(int id, CancellationToken token)
    {
        var result = await mediator.Send(new GetPilotByIdQuery(id), token);

        return result.Match<IActionResult>(
            success => Ok(success),
            notFound => NotFound(),
            error => BadRequest("An error occurred.")
        );
    }

    [HttpPost]
    [SwaggerOperation(Tags = new[] { OPNAME })]
    [SwaggerResponse(201, "Pilot created successfully.")]
    [SwaggerResponse(400, "Invalid pilot data.")]
    public async Task<IActionResult> Create([FromBody] PilotCreateDto pilot, CancellationToken token)
    {
        await mediator.Send(new CreatePilotCommand(pilot), token);
        return CreatedAtAction(nameof(GetById), new { id = pilot.Id }, pilot);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Tags = new[] { OPNAME })]
    [SwaggerResponse(204, "Pilot updated successfully.")]
    [SwaggerResponse(400, "Invalid pilot data.")]
    [SwaggerResponse(404, "Pilot not found.")]
    public async Task<IActionResult> Update(int id, [FromBody] PilotUpdateDto updateDto, CancellationToken token)
    {
        await mediator.Send(new UpdatePilotCommand(id, updateDto), token);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Tags = new[] { OPNAME })]
    [SwaggerResponse(204, "Pilot deleted successfully.")]
    [SwaggerResponse(404, "Pilot not found.")]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> Delete(int id, CancellationToken token)
    {
        await mediator.Send(new DeletePilotCommand(id), token);
        return NoContent();
    }
}