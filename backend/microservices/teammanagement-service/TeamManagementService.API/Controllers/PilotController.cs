﻿using Microsoft.AspNetCore.Mvc;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.CQRS.Pilots.Queries;
using TeamManagementService.Application.CQRS.Pilots.Commands;
using Race.Shared.Utilities.Paging;

namespace TeamManagementService.API.Controllers;

[ApiController]
[Route("api/pilots")]
public class PilotController(IMediator mediator, ILogger<PilotController> logger) : ControllerBase
{
    private const string OPNAME = "Pilots";

    [HttpGet]
    [SwaggerOperation(Tags = new[] { OPNAME })]
    [SwaggerResponse(200, "Returns a paginated list of pilots.")]
    [SwaggerResponse(400, "Invalid pagination parameters.")]
    public async Task<IActionResult> GetAll([FromQuery] PagerParameters pagerParameters, [FromQuery] PilotFilterDto filterDto, CancellationToken token)
    {
        var result = await mediator.Send(new GetAllPilotsQuery(pagerParameters, filterDto), token);

        return result.Match<IActionResult>(
            Ok,
            notFound => NotFound(new { message = "Pilot not found"}),
            error => BadRequest(StatusCodes.Status500InternalServerError));
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Tags = new[] { OPNAME })]
    [SwaggerResponse(200, "Returns the pilot details.")]
    [SwaggerResponse(404, "Pilot not found.")]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> GetById(int id, CancellationToken token)
    {
        logger.LogInformation("Request received");
        var result = await mediator.Send(new GetPilotByIdQuery(id), token);

        return result.Match<IActionResult>(
            Ok,
            notFound => NotFound(),
            error => BadRequest("An error occurred.")
        );
    }

    [HttpPost]
    [SwaggerOperation(Tags = new[] { OPNAME })]
    [SwaggerResponse(201, "Pilot created successfully.")]
    [SwaggerResponse(400, "Invalid pilot data.")]
    public async Task<IActionResult> Post([FromBody] PilotCreateDto pilotToCreate, CancellationToken token)
    {
        var createdPilot = await mediator.Send(new CreatePilotCommand(pilotToCreate), token);
        return CreatedAtAction(nameof(GetById), new { id = createdPilot.Id }, createdPilot);
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
        logger.LogInformation("Request received");
        var result = await mediator.Send(new DeletePilotCommand(id), token);

        return result.Match<IActionResult>(
            deletedId => NoContent(),
            notFound => NotFound(new { error = notFound.ToString() }),
            error => BadRequest(new { error = error.ToString() })
        );
    }

    //[HttpPost("batch")]
    //[SwaggerOperation(Tags = new[] { OPNAME })]
    //[SwaggerResponse(201, "Pilots created successfully.")]
    //[SwaggerResponse(400, "Invalid pilot data.")]
    //public async Task<IActionResult> CreateBatch([FromBody] IEnumerable<PilotCreateDto> pilots, CancellationToken token)
    //{
    //    var pilotList = pilots.ToList();
    //    var results = new List<PilotListDto>();

    //    foreach (var pilot in pilotList)
    //    {
    //        var result = await mediator.Send(new CreatePilotCommand(pilot), token);
    //        results.Add(result);
    //    }

    //    return CreatedAtAction(nameof(GetAll), results);
    //}
}