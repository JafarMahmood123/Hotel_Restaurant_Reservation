using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.Hotel;
using Hotel_Restaurant_Reservation.Application.DTOs.HotelDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.DeleteHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.UpdateHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAllHotels;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetHotelById;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class HotelsController : ApiController
{
    private readonly IMapper _mapper;

    public HotelsController(ISender sender, IMapper mapper) : base(sender)
    {
        _mapper = mapper;
    }

    [HttpGet("{id:guid}")]
    [ActionName("GetHotelById")]
    public async Task<IActionResult> GetHotelById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetHotelByIdQuery(id);

        var hotel = await Sender.Send(query, cancellationToken);

        if (hotel is null)
            return NotFound();

        var hotelResponse = _mapper.Map<HotelResponse>(hotel);

        return Ok(hotelResponse);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHotel(CancellationToken cancellationToken)
    {
        var query = new GetAllHotelsQuery();

        var hotels = await Sender.Send(query, cancellationToken);

        IEnumerable<HotelResponse> hotelResponses = _mapper.Map<IEnumerable<HotelResponse>>(hotels);

        return Ok(hotelResponses);
    }

    [HttpPost]
    public async Task<IActionResult> AddHotel(HotelAddRequest hotelAddRequest, CancellationToken cancellationToken)
    {
        Hotel requestedHotel = _mapper.Map<Hotel>(hotelAddRequest);

        var command = new AddHotelCommand(requestedHotel);

        var hotel = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, hotel);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateHotel([FromRoute] Guid id, [FromBody] UpdateHotelRequest updateHotelRequest, CancellationToken cancellationToken)
    {
        var command = new UpdateHotelCommand(id, updateHotelRequest);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteHotel(Guid hotelId, CancellationToken cancellationToken)
    {
        var command = new DeleteHotelCommand(hotelId);

        var hotelResponse = await Sender.Send(command, cancellationToken);

        if (hotelResponse == null)
            return NotFound();

        return Ok(hotelResponse);
    }
}