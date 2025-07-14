using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.AddLocation;
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
}