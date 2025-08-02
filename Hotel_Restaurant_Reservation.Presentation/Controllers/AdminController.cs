using Hotel_Restaurant_Reservation.Application.Implementation.Admins.Commands.AddAdmin;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class AdminController : ApiController
{
    public AdminController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> AddAdmin([FromBody] AddAdminRequest request, CancellationToken cancellationToken)
    {
        var command = new AddAdminCommand(request);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result);
    }
}
