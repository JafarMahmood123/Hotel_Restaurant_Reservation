//using AutoMapper;
//using Hotel_Restaurant_Reservation.Application.DTOs.LocalLocationDTOs;
//using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.AddLocalLocations;
//using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.DeleteLocalLocation;
//using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.UpdateLocalLocation;
//using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetAllLocalLocations;
//using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationById;
//using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationByName;
//using Hotel_Restaurant_Reservation.Domain.Entities;
//using Hotel_Restaurant_Reservation.Presentation.Abstractions;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;

//namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

//public class LocalLocationController : ApiController
//{
//    private readonly IMapper mapper;

//    public LocalLocationController(ISender sender, IMapper mapper) : base(sender)
//    {
//        this.mapper = mapper;
//    }

//    [HttpGet]
//    public async Task<IActionResult> GetAllLocalLocations(CancellationToken cancellationToken)
//    {
//        var query = new GetAllLocalLocationsQuery();

//        var localLocations = await Sender.Send(query, cancellationToken);

//        var localLocationsResponse = mapper.Map<IEnumerable<LocalLocationResponse>>(localLocations);

//        return Ok(localLocationsResponse);
//    }

//    [HttpGet]
//    [Route("{id:guid}")]
//    public async Task<IActionResult> GetLocalLoactionById(Guid id, CancellationToken cancellationToken)
//    {
//        var query = new GetLocalLocationByIdQuery(id);

//        var localLocation = await Sender.Send(query, cancellationToken);

//        if (localLocation == null)
//            return NotFound();

//        var localLocationResponse = mapper.Map<LocalLocationResponse>(localLocation);

//        return Ok(localLocationResponse);
//    }

//    [HttpGet]
//    [Route("name:string")]
//    public async Task<IActionResult> GetLocalLocationByName(string name, CancellationToken cancellationToken)
//    {
//        var query = new GetLocalLocationByNameQuery(name);

//        var localLocation = await Sender.Send(query, cancellationToken);

//        if (localLocation == null)
//            return NotFound();

//        var localLocationResponse = mapper.Map<LocalLocationResponse>(localLocation);
//        return Ok(localLocationResponse);
//    }

//    [HttpPost]
//    public async Task<IActionResult> AddLocalLocation(AddLocalLocationRequest addlocalLocationRequest, CancellationToken cancellationToken)
//    {
//        var localLocation = mapper.Map<LocalLocation>(addlocalLocationRequest);
//        var query = new AddLocalLocationCommand(localLocation, addlocalLocationRequest.CityId);

//        localLocation = await Sender.Send(query, cancellationToken);

//        var localLocationResponse = mapper.Map<LocalLocationResponse>(localLocation);

//        return CreatedAtAction(nameof(GetLocalLoactionById), new { id = localLocation.Id }, localLocationResponse);
//    }

//    [HttpPut]
//    [Route("{id:guid}")]
//    public async Task<IActionResult> UpdateLocalLocation([FromRoute] Guid id, [FromBody] UpdateLocalLocationRequest updateLocalLocationRequest, CancellationToken cancellationToken)
//    {
//        var localLocation = mapper.Map<LocalLocation>(updateLocalLocationRequest);

//        var query = new UpdateLocalLocationCommand(id, localLocation);

//        localLocation = await Sender.Send(query, cancellationToken);

//        if (localLocation == null)
//            return NotFound();

//        var localLocationResponse = mapper.Map<LocalLocationResponse>(localLocation);

//        return Ok(localLocationResponse);
//    }

//    [HttpDelete]
//    [Route("{id:guid}")]
//    public async Task<IActionResult> DeleteLocalLocation([FromRoute] Guid id, CancellationToken cancellationToken)
//    {
//        var command = new DeleteLocalLocationCommand(id);

//        var localLocation = await Sender.Send(command, cancellationToken);

//        if (localLocation == null)
//            return NotFound();

//        var localLocationResponse = mapper.Map<LocalLocationResponse>(localLocation);

//        return Ok(localLocationResponse);
//    }
//}
