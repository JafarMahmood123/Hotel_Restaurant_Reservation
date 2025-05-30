using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.LocationDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.AddLocation;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.DeleteLcoation;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.UpdateLocation;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries.GetAllLocations;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries.GetLocationById;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class LocationController : ApiController
{
    private readonly IMapper mapper;

    public LocationController(ISender sender, IMapper mapper) : base(sender)
    {
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLocations(CancellationToken cancellationToken)
    {
        var query = new GetAllLocationsQuery();

        var locations = await Sender.Send(query, cancellationToken);

        var locationsResponse = mapper.Map<IEnumerable<LocationResponse>>(locations);

        return Ok(locationsResponse);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetLocationById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetLocationByIdQuery(id);

        var location = await Sender.Send(query, cancellationToken);

        if (location == null)
            return NotFound();

        var locationResponse = mapper.Map<LocationResponse>(location);

        return Ok(locationResponse);
    }

    [HttpPost]
    public async Task<IActionResult> AddLocation(AddLocationRequest addLocationRequest, CancellationToken cancellationToken)
    {
        var location = mapper.Map<Location>(addLocationRequest);

        var query = new AddLocationCommand(location);

        location = await Sender.Send(query, cancellationToken);

        var locationResponse = mapper.Map<LocationResponse>(location);

        return CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, locationResponse);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateLocation([FromRoute] Guid id, [FromBody] UpdateLocationRequest updateLocationRequest, CancellationToken cancellationToken)
    {
        var location = mapper.Map<Location>(updateLocationRequest);

        var query = new UpdateLocationCommand(id, location);

        location = await Sender.Send(query, cancellationToken);

        if (location == null)
            return NotFound();

        var locationResponse = mapper.Map<LocationResponse>(location);

        return Ok(locationResponse);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteCity([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteLcoationCommand(id);

        var location = await Sender.Send(command, cancellationToken);

        if (location == null)
            return NotFound();

        var locationResponse = mapper.Map<LocationResponse>(location);

        return Ok(locationResponse);
    }
}
