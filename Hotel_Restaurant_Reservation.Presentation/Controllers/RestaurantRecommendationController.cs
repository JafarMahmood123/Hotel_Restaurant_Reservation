using Hotel_Restaurant_Reservation.Application.Implementation.Recommendations.Queries.GetRecommendedRestaurantsBasedOnUserId;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class RestaurantRecommendationController : ApiController
{
    public RestaurantRecommendationController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetRecommendedRestaurants(string userId, CancellationToken cancellationToken)
    {
        var query = new GetRecommendedRestaurantsBasedOnUserIdQuery(userId);

        var restult = await Sender.Send(query, cancellationToken);

        if (restult.IsFailure)
            return BadRequest(restult.Error);

        return Ok(restult.Value);
    }
}
