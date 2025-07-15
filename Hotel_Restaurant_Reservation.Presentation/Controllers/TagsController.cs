using Hotel_Restaurant_Reservation.Application.DTOs.TagDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Commands.AddTag;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries.GetAllTags;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries.GetTagsByRestaurantId;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class TagsController : ApiController
    {
        public TagsController(ISender sender) : base(sender)
        {
        }

        [HttpPost]
        public async Task<IActionResult> AddTag([FromBody] AddTagRequest addTagRequest, CancellationToken cancellationToken)
        {
            var command = new AddTagCommand(addTagRequest);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags(CancellationToken cancellationToken)
        {
            var query = new GetAllTagsQuery();
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("restaurant/{restaurantId:guid}")]
        public async Task<IActionResult> GetTagsByRestaurantId(Guid restaurantId, CancellationToken cancellationToken)
        {
            var query = new GetTagsByRestaurantIdQuery(restaurantId);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }
}