using Hotel_Restaurant_Reservation.Application.Implementation.EventReviews.Commands.AddEventReview;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
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
    }
}