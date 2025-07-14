using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.AddLocalLocations;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.DeleteLocalLocation;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.UpdateLocalLocation;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class LocalLocationController : ApiController
{
    public LocalLocationController(ISender sender) : base(sender)
    {
    }

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
}