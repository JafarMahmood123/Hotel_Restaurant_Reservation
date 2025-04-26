using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CountryDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.AddCountry;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.DeleteCountry;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.UpdateCountry;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetAllCountries;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetCountryById;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetCountryByName;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class CountriesController : ApiController
{
    private readonly IMapper mapper;

    public CountriesController(ISender sender, IMapper mapper) : base(sender)
    {
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCountries(CancellationToken cancellationToken)
    {
        var query = new GetAllCountriesQuery();

        var countries = await Sender.Send(query, cancellationToken);

        var countriesResponse = mapper.Map<IEnumerable<CountryResponse>>(countries);

        return Ok(countriesResponse);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetCountryById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCountryByIdQuery(id);

        var country = await Sender.Send(query, cancellationToken);

        if (country == null)
            return NotFound();

        var countryResponse = mapper.Map<CountryResponse>(country);

        return Ok(countryResponse);
    }

    [HttpGet]
    [Route("name:string")]
    public async Task<IActionResult> GetCountryByName(string name, CancellationToken cancellationToken)
    {
        var query = new GetCountryByNameQuery(name);

        var country = await Sender.Send(query, cancellationToken);

        if (country == null)
            return NotFound();

        var countryResponse = mapper.Map<CountryResponse>(country);
        return Ok(countryResponse);
    }

    [HttpPost]
    public async Task<IActionResult> AddCountry(AddCountryRequest addCityRequest, CancellationToken cancellationToken)
    {
        var country = mapper.Map<Country>(addCityRequest);
        var query = new AddCountryCommand(country);

        country = await Sender.Send(query, cancellationToken);

        var countryResponse = mapper.Map<CountryResponse>(country);

        return CreatedAtAction(nameof(GetCountryById), new { id = country.Id }, countryResponse);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateCountry([FromRoute] Guid id, [FromBody] UpdateCountryRequest updateCountryRequest, CancellationToken cancellationToken)
    {
        var country = mapper.Map<Country>(updateCountryRequest);

        var query = new UpdateCountryCommand(id, country);

        country = await Sender.Send(query, cancellationToken);

        if (country == null)
            return NotFound();

        var countryResponse = mapper.Map<CountryResponse>(country);

        return Ok(countryResponse);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteCountry([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCountryCommand(id);

        var country = await Sender.Send(command, cancellationToken);

        if (country == null)
            return NotFound();

        var countryResponse = mapper.Map<CountryResponse>(country);

        return Ok(countryResponse);
    }
}
