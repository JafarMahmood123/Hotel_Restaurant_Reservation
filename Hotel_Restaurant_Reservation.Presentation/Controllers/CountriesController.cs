// Hotel_Restaurant_Reservation.Presentation/Controllers/CountriesController.cs
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.AddCountry;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.DeleteCountry;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.UpdateCountry;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetAllCountries;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetCountryById;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class CountriesController : ApiController
{
    public CountriesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCountries(CancellationToken cancellationToken)
    {
        var query = new GetAllCountriesQuery();
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ActionName(nameof(GetCountryById))]
    public async Task<IActionResult> GetCountryById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCountryByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> AddCountry([FromBody] AddCountryRequest addCountryRequest, CancellationToken cancellationToken)
    {
        var command = new AddCountryCommand(addCountryRequest);
        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetCountryById), new { id = result.Value.Id }, result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCountry(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCountryCommand(id);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCountry(Guid id, [FromBody] UpdateCountryRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCountryCommand(id, request);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }
}