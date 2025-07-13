using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Commands.AddCurrencyType;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class CurrencyTypesController : ApiController
{
    public CurrencyTypesController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> AddCurrencyType([FromBody] AddCurrencyTypeRequest addCurrencyTypeRequest, CancellationToken cancellationToken)
    {
        var command = new AddCurrencyTypeCommand(addCurrencyTypeRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}