using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Commands.AddCurrencyType;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Commands.DeleteCurrencyType;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Commands.UpdateCurrencyType;
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCurrencyType(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCurrencyTypeCommand(id);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCurrencyType(Guid id, [FromBody] UpdateCurrencyTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCurrencyTypeCommand(id, request);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }
}