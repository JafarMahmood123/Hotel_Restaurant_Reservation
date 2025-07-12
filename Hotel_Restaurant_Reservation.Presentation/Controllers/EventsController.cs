using Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.AddEvent;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.DeleteEvent;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.UpdateEvent;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class EventsController : ApiController
    {
        public EventsController(ISender sender) : base(sender)
        {
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] AddEventRequest addEventRequest, CancellationToken cancellationToken)
        {
            var command = new AddEventCommand(addEventRequest);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEvent(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteEventCommand(id);
            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateEventCommand(id, request);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }
}