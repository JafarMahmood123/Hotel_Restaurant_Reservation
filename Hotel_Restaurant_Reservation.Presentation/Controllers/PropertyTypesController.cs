using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Commands.AddPropertyType;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Commands.DeletePropertyType;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Commands.UpdatePropertyType;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Queries.GetAllPropertyTypes;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Queries.GetPropertyTypeById;
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

    [HttpGet]
    public async Task<IActionResult> GetAllPropertyTypes(CancellationToken cancellationToken)
    {
        var query = new GetAllPropertyTypesQuery();
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetPropertyTypeById))]
    public async Task<IActionResult> GetPropertyTypeById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPropertyTypeByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
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
        return CreatedAtAction(nameof(GetPropertyTypeById), new { id = result.Value.Id }, result.Value);
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