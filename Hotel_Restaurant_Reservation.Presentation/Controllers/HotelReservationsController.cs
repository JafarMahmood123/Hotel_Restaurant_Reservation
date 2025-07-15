using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.AddHotelReservation;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.DeleteHotelReservation;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.UpdateHotelReservation;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries.GetAllHotelReservationsByCustomerId;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries.GetAllHotelReservationsByHotelId;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries.GetHotelReservationById;
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

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateHotelReservation(Guid id, [FromBody] UpdateHotelReservationRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateHotelReservationCommand(id, request);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("customer/{customerId:guid}")]
        public async Task<IActionResult> GetAllHotelReservationsByCustomerId(Guid customerId, CancellationToken cancellationToken)
        {
            var query = new GetAllHotelReservationsByCustomerIdQuery(customerId);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("hotel/{hotelId:guid}")]
        public async Task<IActionResult> GetAllHotelReservationsByHotelId(Guid hotelId, CancellationToken cancellationToken)
        {
            var query = new GetAllHotelReservationsByHotelIdQuery(hotelId);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetHotelReservationById))]
        public async Task<IActionResult> GetHotelReservationById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetHotelReservationByIdQuery(id);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
