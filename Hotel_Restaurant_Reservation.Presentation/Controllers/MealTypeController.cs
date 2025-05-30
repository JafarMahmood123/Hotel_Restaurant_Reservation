using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Commands;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class MealTypeController : ApiController
{
    public MealTypeController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> AddMealType([FromBody] AddMealTypeRequest addMealTypeRequest, CancellationToken cancellationToken)
    {
        var command = new AddMealTypeCommand(addMealTypeRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);


        return Ok(result.Value);
    }
}
