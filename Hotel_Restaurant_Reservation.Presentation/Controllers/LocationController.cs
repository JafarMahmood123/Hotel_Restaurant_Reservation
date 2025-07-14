using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.AddLocation;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.DeleteLocation;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.UpdateLocation;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries.GetAllLocations;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries.GetLocationById;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class LocationController : ApiController
{
    public LocationController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> AddLocation([FromBody] AddLocationRequest addLocationRequest, CancellationToken cancellationToken)
    {
        var command = new AddLocationCommand(addLocationRequest);
        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateLocation(Guid id, [FromBody] UpdateLocationRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateLocationCommand(id, request);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteLocation(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteLocationCommand(id);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLocations(CancellationToken cancellationToken)
    {
        var query = new GetAllLocationsQuery();
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetLocationById))]
    public async Task<IActionResult> GetLocationById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetLocationByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }
}