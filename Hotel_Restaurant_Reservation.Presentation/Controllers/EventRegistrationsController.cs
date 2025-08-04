using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Commands.AddEventRegistration;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Commands.DeleteEventRegistration;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Commands.UpdateEventRegistration;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries.GetAllEventRegistration;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries.GetAllEventRegistrationsByCustomerId;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries.GetAllEventRegistrationsByEventId;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries.GetEventRegistrationById;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class EventRegistrationsController : ApiController
    {
        public EventRegistrationsController(ISender sender) : base(sender)
        {
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> AddEventRegistration([FromBody] AddEventRegistrationRequest addEventRegistrationRequest, CancellationToken cancellationToken)
        {
            var command = new AddEventRegistrationCommand(addEventRegistrationRequest);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEventRegistration(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteEventRegistrationCommand(id);
            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEventRegistration(Guid id, [FromBody] UpdateEventRegistrationRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateEventRegistrationCommand(id, request);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> GetAllEventRegistrations(CancellationToken cancellationToken)
        {
            var query = new GetAllEventRegistrationsQuery();
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetEventRegistrationById))]
        public async Task<IActionResult> GetEventRegistrationById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetEventRegistrationByIdQuery(id);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("customer/{customerId:guid}")]
        public async Task<IActionResult> GetAllEventRegistrationsByCustomerId(Guid customerId, CancellationToken cancellationToken)
        {
            var query = new GetAllEventRegistrationsByCustomerIdQuery(customerId);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("event/{eventId:guid}")]
        public async Task<IActionResult> GetAllEventRegistrationsByEventId(Guid eventId, CancellationToken cancellationToken)
        {
            var query = new GetAllEventRegistrationsByEventIdQuery(eventId);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }
}