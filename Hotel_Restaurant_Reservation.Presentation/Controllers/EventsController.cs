using Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.AddEvent;
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
    }
}