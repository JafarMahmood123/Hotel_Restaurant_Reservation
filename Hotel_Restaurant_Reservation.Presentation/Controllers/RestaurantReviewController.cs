using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Commands.AddReview;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class RestaurantReviewController : ApiController
{

    public RestaurantReviewController(ISender sender) : base(sender)
    {
    }

    [HttpPost("AddReview")]
    public async Task<IActionResult> AddReview([FromBody] AddRestaurantReviewRequest addRestaurantReviewRequest, CancellationToken cancellationToken)
    {
        var command = new AddRestaurantReviewCommand(addRestaurantReviewRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
