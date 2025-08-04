using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Commands.AddAmenity;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Commands.DeleteAmenity;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Commands.UpdateAmenity;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries.GetAllAmenities;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries.GetAmenityById;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class AmenitiesController : ApiController
    {
        public AmenitiesController(ISender sender) : base(sender)
        {
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAmenity([FromBody] AddAmenityRequest addAmenityRequest, CancellationToken cancellationToken)
        {
            var command = new AddAmenityCommand(addAmenityRequest);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAmenity(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteAmenityCommand(id);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAmenity(Guid id, [FromBody] UpdateAmenityRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateAmenityCommand(id, request);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAmenities(CancellationToken cancellationToken)
        {
            var query = new GetAllAmenitiesQuery();
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetAmenityById))]
        public async Task<IActionResult> GetAmenityById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetAmenityByIdQuery(id);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }
}