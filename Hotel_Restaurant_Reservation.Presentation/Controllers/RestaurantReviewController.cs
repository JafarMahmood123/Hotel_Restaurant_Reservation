﻿using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Commands.AddReview;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Queries.GetAllRestaurantReviews;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Queries.GetAllRestaurantReviewsByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Queries.GetAllRestaurantReviewsByUserId;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> GetAllRestaurantReviews(CancellationToken cancellationToken)
        {
            var query = new GetAllRestaurantReviewsQuery();
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("restaurant/{restaurantId:guid}")]
        public async Task<IActionResult> GetAllRestaurantReviewsByRestaurantId(Guid restaurantId, CancellationToken cancellationToken)
        {
            var query = new GetAllRestaurantReviewsByRestaurantIdQuery(restaurantId);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetAllRestaurantReviewsByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetAllRestaurantReviewsByUserIdQuery(userId);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
