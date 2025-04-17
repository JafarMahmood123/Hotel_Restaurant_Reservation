using Hotel_Restaurant_Reservation.Application.Abstractions.Hotels.Queries.GetHotelById;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class HotelsController : ApiController
{
    public async Task<IActionResult> GetHotelById(Guid hotelId, CancellationToken cancellationToken)
    {
        var query = new GetHotelByIdQuery(hotelId);

        var hotel = await Sender.Send(query, cancellationToken);

        return Ok(hotel);
    }
}
