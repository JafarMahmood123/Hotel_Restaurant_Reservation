using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Commands.AddEventReview;
using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Commands.DeleteEventReview;
using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Commands.UpdateEventReview;
using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries.GetAllEventReviews;
using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries.GetEventReviewById;
using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Queries.GetAllEventReviewsByEventId;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class EventReviewsController : ApiController
{
    public EventReviewsController(ISender sender) : base(sender)
    {
    }

    [HttpPost("AddReview")]
    public async Task<IActionResult> AddEventReview([FromBody] AddEventReviewRequest addEventReviewRequest, CancellationToken cancellationToken)
    {
        var command = new AddEventReviewCommand(addEventReviewRequest);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEventReview(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteEventReviewCommand(id);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEventReview(Guid id, [FromBody] UpdateEventReviewRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateEventReviewCommand(id, request);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEventReviews(CancellationToken cancellationToken)
    {
        var query = new GetAllEventReviewsQuery();
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetEventReviewById))]
    public async Task<IActionResult> GetEventReviewById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetEventReviewByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpGet("event/{eventId:guid}")]
    public async Task<IActionResult> GetAllEventReviewsByEventId(Guid eventId, CancellationToken cancellationToken)
    {
        var query = new GetAllEventReviewsByEventIdQuery(eventId);
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }
}