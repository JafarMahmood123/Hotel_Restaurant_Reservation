using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.Hotel;
using Hotel_Restaurant_Reservation.Application.DTOs.HotelDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAllHotels;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetHotelById;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using Hotel_Restaurant_Reservation.Presentation.Profiles;
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

    [HttpGet("{hotelId:guid}")]
    [ActionName("GetHotelById")]
    public async Task<IActionResult> GetHotelById(Guid hotelId, CancellationToken cancellationToken)
    {
        var query = new GetHotelByIdQuery(hotelId);

        var hotel = await Sender.Send(query, cancellationToken);

        if (hotel is null)
            return NotFound();

        var hotelResponse = _mapper.Map<HotelProfile>(hotel);

        return Ok(hotelResponse);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHotel(CancellationToken cancellationToken)
    {
        var query = new GetAllHotelsQuery();

        var hotels = await Sender.Send(query, cancellationToken);

        IEnumerable<HotelResponse> hotelResponses = new List<HotelResponse>();

        if (hotels != null)
        {
            foreach (Hotel hotel in hotels)
            {
                hotelResponses.Append(_mapper.Map<HotelResponse>(hotel));
            }
        }

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
}
