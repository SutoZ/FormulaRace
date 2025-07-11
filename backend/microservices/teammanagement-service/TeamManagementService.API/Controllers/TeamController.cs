using MediatR;
using Microsoft.AspNetCore.Mvc;
using Race.Shared.Utilities.Paging;
using Swashbuckle.AspNetCore.Annotations;
using TeamManagementService.Application.CQRS.Teams.Commands;
using TeamManagementService.Application.CQRS.Teams.Queries;
using TeamManagementService.Application.Dtos.Teams;

namespace TeamManagementService.API.Controllers;

[ApiController]
[Route("api/teams")]
public class TeamController(IMediator mediator) : ControllerBase
{
    private const string OPNAME = "Teams";

    [HttpGet]
    [SwaggerOperation(Tags = new string[] { OPNAME })]
    [SwaggerResponse(200, "Returns a paginated list of teams.")]
    [SwaggerResponse(400, "Invalid pagination parameters.")]
    public async Task<IActionResult> GetAll([FromQuery] PagerParameters pagerParameters, CancellationToken token)
    {
        var result = await mediator.Send(new GetAllTeamsQuery(pagerParameters), token);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Tags = new[] { OPNAME })]
    [SwaggerResponse(200, "Returns the team details.")]
    [SwaggerResponse(404, "Team not found.")]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> GetById(int id, CancellationToken token)
    {
        var result = await mediator.Send(new GetTeamByIdQuery(id), token);
        return result.Match<IActionResult>(
            success => Ok(success),
            notFound => NotFound(),
            error => BadRequest("An error occurred while retrieving the team.")
        );
    }

    [HttpPost]
    [SwaggerOperation(Tags = new[] { OPNAME })]
    [SwaggerResponse(201, "Team created successfully.")]
    [SwaggerResponse(400, "Invalid team data.")]
    [SwaggerResponse(409, "Team with the same name already exists.")]
    public async Task<ActionResult<int>> Create([FromBody] TeamCreateDto createDto, CancellationToken token)
    {
        var result = await mediator.Send(new CreateTeamCommand(createDto), token);
        return CreatedAtAction(nameof(GetById), new { id = createDto.Id }, result);
    }


    [HttpPut("{id}")]
    [SwaggerOperation(Tags = new[] { OPNAME })]
    [SwaggerResponse(204, "Team updated successfully.")]
    [SwaggerResponse(400, "Invalid team data.")]
    [SwaggerResponse(404, "Team not found.")]
    public async Task<IActionResult> Update(int id, [FromBody] TeamUpdateDto updateDto, CancellationToken token)
    {
        await mediator.Send(new UpdateTeamCommand(id, updateDto), token);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Tags = new[] { OPNAME })]
    [SwaggerResponse(204, "Team deleted successfully.")]
    [SwaggerResponse(404, "Team not found.")]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> Delete(int id, CancellationToken token)
    {
        await mediator.Send(new DeleteTeamCommand(id), token);
        return NoContent();
    }
}