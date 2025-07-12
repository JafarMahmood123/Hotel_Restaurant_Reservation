using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Commands.AddEventRegistration;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class EventRegistrationsController : ApiController
    {
        public EventRegistrationsController(ISender sender) : base(sender)
        {
        }

        [HttpPost]
        public async Task<IActionResult> AddEventRegistration([FromBody] AddEventRegistrationRequest addEventRegistrationRequest, CancellationToken cancellationToken)
        {
            var command = new AddEventRegistrationCommand(addEventRegistrationRequest);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}