using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.AddHotelReview;
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
    }
}