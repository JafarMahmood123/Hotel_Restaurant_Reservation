using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.AddHotelReview;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.DeleteHotelReview;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.UpdateHotelReview;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries.GetAllHotelReviews;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries.GetAllHotelReviewsByHotelId;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries.GetAllHotelReviewsByUserId;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries.GetHotelReviewById;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class HotelReviewsController : ApiController
    {
        public HotelReviewsController(ISender sender) : base(sender)
        {
        }

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
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

        [HttpGet]
        public async Task<IActionResult> GetAllHotelReviews(CancellationToken cancellationToken)
        {
            var query = new GetAllHotelReviewsQuery();
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetHotelReviewById))]
        public async Task<IActionResult> GetHotelReviewById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetHotelReviewByIdQuery(id);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("hotel/{hotelId:guid}")]
        public async Task<IActionResult> GetAllHotelReviewsByHotelId(Guid hotelId, CancellationToken cancellationToken)
        {
            var query = new GetAllHotelReviewsByHotelIdQuery(hotelId);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetAllHotelReviewsByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetAllHotelReviewsByUserIdQuery(userId);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
