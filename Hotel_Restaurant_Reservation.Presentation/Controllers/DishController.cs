using Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Commands.AddDish;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class DishController : ApiController
{
    public DishController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> AddDish([FromBody] AddDishRequest addDishRequest, CancellationToken cancellationToken)
    {
        var command = new AddDishCommand(addDishRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);


        return Ok(result.Value);
    }
}
