using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CityDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.AddCity;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.DeleteCity;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.UpdateCity;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetAllCities;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetCityById;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetCityByName;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class CitiesController : ApiController
{
    private readonly IMapper mapper;

    public CitiesController(ISender sender, IMapper mapper) : base(sender)
    {
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCities(CancellationToken cancellationToken)
    {
        var query = new GetAllCitiesQuery();

        var cities = await Sender.Send(query, cancellationToken);

        var citiesResponse = mapper.Map<IEnumerable<CityResponse>>(cities);

        return Ok(citiesResponse);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetCityById (Guid id,  CancellationToken cancellationToken)
    {
        var query = new GetCityByIdQuery(id);

        var city = await Sender.Send(query, cancellationToken);

        if(city == null) 
            return NotFound();

        var cityResponse = mapper.Map<CityResponse>(city);

        return Ok(cityResponse);
    }

    [HttpGet]
    [Route("name:string")]
    public async Task<IActionResult> GetCityByName(string name, CancellationToken cancellationToken)
    {
        var query = new GetCityByNameQuery(name);

        var city = await Sender.Send(query, cancellationToken);

        if (city == null)
            return NotFound();

        var cityResponse = mapper.Map<CityResponse>(city);

        return Ok(cityResponse);
    }

    [HttpPost]
    public async Task<IActionResult> AddCity(AddCityRequest addCityRequest, CancellationToken cancellationToken)
    {
        var city = mapper.Map<City>(addCityRequest);

        var query = new AddCityCommand(city, city.CountryId);

        city = await Sender.Send(query, cancellationToken);

        var cityResponse = mapper.Map<CityResponse>(city);

        return CreatedAtAction(nameof(GetCityById), new { id = city.Id }, cityResponse);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateCity([FromRoute] Guid id, [FromBody] UpdateCityRequest updateCityRequest, CancellationToken cancellationToken)
    {
        var city = mapper.Map<City>(updateCityRequest);

        var query = new UpdateCityCommand(id, city);

        city = await Sender.Send(query, cancellationToken);

        if(city == null)
            return NotFound();

        var cityResponse = mapper.Map<CityResponse>(city);

        return Ok(cityResponse);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteCity([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCityCommand(id);

        var city = await Sender.Send(command, cancellationToken);

        if(city == null)
            return NotFound();

        var cityResponse = mapper.Map<CityResponse>(city);

        return Ok(cityResponse);
    }
}
