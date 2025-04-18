using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAllHotels;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetHotelById;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class HotelsController : ApiController
{
    public HotelsController(ISender sender) : base(sender)
    {
        
    }

    [HttpGet("{hotelId:guid}")]
    public async Task<IActionResult> GetHotelById(Guid hotelId, CancellationToken cancellationToken)
    {
        var query = new GetHotelByIdQuery(hotelId);

        var hotel = await Sender.Send(query, cancellationToken);

        return Ok(hotel);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHotel(CancellationToken cancellationToken)
    {
        var query = new GetAllHotelsQuery();

        var hotels = await Sender.Send(query, cancellationToken);

        return Ok(hotels);
    }
}
