using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Inventory.Api.Features.Commands.CreateTruckWithStatus;
using Vehicle.Inventory.Api.Features.Commands.UpdateTruckStatus;
using Vehicle.Inventory.Api.Features.Queries.GetTruckInCurrentStatus;
using Vehicle.Inventory.Api.Features.Queries.GetTrucksFilteredByCriteria;
using Vehicle.Inventory.Infrastructure.Errors.Exceptions;

namespace Vehicle.Inventory.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class TrucksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrucksController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("{Id}", Name = nameof(GetTruckInCurrentStatus))]
    public async Task<IActionResult> GetTruckInCurrentStatus(string id)
    {
        var query = new GetTruckInCurrentStatusQuery(id);

        var result = await _mediator.Send(query);

        return result.Match(
                            truck => Ok(truck),
                            ex => ex switch
                                    {
                                        DataNotFoundException _ => NotFound(),
                                        _ => StatusCode(500) as IActionResult
                                    });
    }

    [HttpGet(Name = nameof(GetTrucksFilteredByCriteria))]
    public async Task<IActionResult> GetTrucksFilteredByCriteria([FromQuery] GetTruckFilteredByCriteriaQuery query)
    {
        var result = await _mediator.Send(query);

        if (result is null || !result.Any())
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpPatch(Name = nameof(UpdateStatus))]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateTruckStatusCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
                       truck => Ok(truck),
                       ex => ex switch
                            {
                                DataNotFoundException _ => NotFound(),
                                _ => StatusCode(500) as IActionResult
                            });
    }

    [HttpPost(Name = nameof(CreateTruckWithStatus))]
    public async Task<IActionResult> CreateTruckWithStatus([FromBody] CreateTruckWithStatusCommand command)
    {
        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetTruckInCurrentStatus), new { id = result }, result);
    }
}
