using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.ReviewDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Commands.AddReview;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class ReviewController : ApiController
{
    private readonly IMapper mapper;

    public ReviewController(ISender sender, IMapper mapper) : base(sender)
    {
        this.mapper = mapper;
    }

    [HttpPost("AddReview")]
    public async Task<IActionResult> AddReview([FromBody] AddReviewRequest request, CancellationToken cancellationToken)
    {
        var review = mapper.Map<Review>(request);

        var command = new AddReviewCommand(request.RestaurantId, request.CustomerId, review);

        review = await Sender.Send(command, cancellationToken);

        if(review != null)
        {
            var reviewResponse = mapper.Map<ReviewResponse>(review);

            return Ok(reviewResponse);
        }

        return BadRequest("The description is empty or the rating is not valid.");
    }
}
