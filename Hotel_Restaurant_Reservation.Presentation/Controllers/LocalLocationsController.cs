using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.AddLocalLocations;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.DeleteLocalLocation;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.UpdateLocalLocation;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetAllLocalLocations;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationById;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationByName;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationsByCityId;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class LocalLocationsController : ApiController
{
    public LocalLocationsController(ISender sender) : base(sender)
    {
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddLocalLocation([FromBody] AddLocalLocationRequest addlocalLocationRequest, CancellationToken cancellationToken)
    {
        var command = new AddLocalLocationCommand(addlocalLocationRequest);
        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteLocalLocation(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteLocalLocationCommand(id);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateLocalLocation(Guid id, [FromBody] UpdateLocalLocationRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateLocalLocationCommand(id, request);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLocalLocations(CancellationToken cancellationToken)
    {
        var query = new GetAllLocalLocationsQuery();
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetLocalLocationById))]
    public async Task<IActionResult> GetLocalLocationById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetLocalLocationByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetLocalLocationByName(string name, CancellationToken cancellationToken)
    {
        var query = new GetLocalLocationByNameQuery(name);
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpGet("city/{cityId:guid}")]
    public async Task<IActionResult> GetLocalLocationsByCityId(Guid cityId, CancellationToken cancellationToken)
    {
        var query = new GetLocalLocationsByCityIdQuery(cityId);
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }
}