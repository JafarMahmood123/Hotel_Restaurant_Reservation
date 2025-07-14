// Hotel_Restaurant_Reservation.Presentation/Controllers/CitiesController.cs
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.AddCity;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.DeleteCity;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetAllCities;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetCityById;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class CitiesController : ApiController
{
    public CitiesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCities(CancellationToken cancellationToken)
    {
        var query = new GetAllCitiesQuery();
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ActionName(nameof(GetCityById))]
    public async Task<IActionResult> GetCityById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCityByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> AddCity([FromBody] AddCityRequest addCityRequest, CancellationToken cancellationToken)
    {
        var command = new AddCityCommand(addCityRequest);
        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetCityById), new { id = result.Value.Id }, result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCity(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCityCommand(id);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return NoContent();
    }
}