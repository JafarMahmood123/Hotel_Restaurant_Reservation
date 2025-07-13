using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.AddHotelReview;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.DeleteHotelReview;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.UpdateHotelReview;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class HotelReviewsController : ApiController
    {
        public HotelReviewsController(ISender sender) : base(sender)
        {
        }

        [HttpPost("AddReview")]
        public async Task<IActionResult> AddHotelReview([FromBody] AddHotelReviewRequest addHotelReviewRequest, CancellationToken cancellationToken)
        {
            var command = new AddHotelReviewCommand(addHotelReviewRequest);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteHotelReview(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteHotelReviewCommand(id);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateHotelReview(Guid id, [FromBody] UpdateHotelReviewRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateHotelReviewCommand(id, request);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }
}