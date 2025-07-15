using Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.AddEvent;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.DeleteEvent;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.UpdateEvent;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Queries.GetAllEvents;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Queries.GetEventById;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetEventImages;
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

        [HttpGet]
        public async Task<IActionResult> GetAllEvents(CancellationToken cancellationToken)
        {
            var query = new GetAllEventsQuery();
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetEventById))]
        public async Task<IActionResult> GetEventById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetEventByIdQuery(id);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("{eventId:guid}/images")]
        public async Task<IActionResult> GetEventImages(Guid eventId, CancellationToken cancellationToken)
        {
            var query = new GetEventImagesByEventIdQuery(eventId);
            var result = await Sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }
    }
}