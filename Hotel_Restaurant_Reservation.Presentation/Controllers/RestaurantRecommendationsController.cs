using Hotel_Restaurant_Reservation.Application.Implementation.Recommendations.Queries.GetRecommendedRestaurantsBasedOnUserId;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class RestaurantRecommendationsController : ApiController
{
    public RestaurantRecommendationsController(ISender sender) : base(sender)
    {
    }

    //[Authorize(Roles = "Customer")]
    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetRecommendedRestaurants(
        CancellationToken cancellationToken,
    string userId,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        var query = new GetRecommendedRestaurantsBasedOnUserIdQuery(userId, page, pageSize);

        var result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
