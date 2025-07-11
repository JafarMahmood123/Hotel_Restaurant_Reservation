using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Commands.AddPropertyType;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Commands.DeletePropertyType;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Commands.UpdatePropertyType;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class PropertyTypesController : ApiController
{
    private readonly IMapper _mapper;

    public PropertyTypesController(ISender sender, IMapper mapper) : base(sender)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddPropertyType([FromBody] AddPropertyTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new AddPropertyTypeCommand(request);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return CreatedAtAction(nameof(UpdatePropertyType), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePropertyType(Guid id, [FromBody] UpdatePropertyTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePropertyTypeCommand(id, request);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePropertyType(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeletePropertyTypeCommand(id);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return NoContent();
    }
}