using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.AddHotelReservation;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.DeleteHotelReservation;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class HotelReservationsController : ApiController
    {
        public HotelReservationsController(ISender sender) : base(sender)
        {
        }

        [HttpPost]
        public async Task<IActionResult> AddHotelReservation([FromBody] AddHotelReservationRequest addHotelReservationRequest, CancellationToken cancellationToken)
        {
            var command = new AddHotelReservationCommand(addHotelReservationRequest);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteHotelReservation(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteHotelReservationCommand(id);
            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }
    }
}