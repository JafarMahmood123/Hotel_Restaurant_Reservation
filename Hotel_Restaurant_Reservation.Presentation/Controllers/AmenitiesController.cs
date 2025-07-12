using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Commands.AddAmenity;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Commands.DeleteAmenity;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Commands.UpdateAmenity;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class AmenitiesController : ApiController
    {
        public AmenitiesController(ISender sender) : base(sender)
        {
        }

        [HttpPost]
        public async Task<IActionResult> AddAmenity([FromBody] AddAmenityRequest addAmenityRequest, CancellationToken cancellationToken)
        {
            var command = new AddAmenityCommand(addAmenityRequest);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

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
    }
}