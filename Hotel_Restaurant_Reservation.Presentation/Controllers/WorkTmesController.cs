using Hotel_Restaurant_Reservation.Application.DTOs.WorkTimeDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Commands.AddWorkTime;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class WorkTmesController : ApiController
{
    public WorkTmesController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> AddWotkTime([FromBody] AddWorkTimeRequest addWorkTimeRequest, CancellationToken cancellationToken)
    {
        var command = new AddWorkTimeCommand(addWorkTimeRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
