using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Commands.AddCuisine;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Commands.DeleteCuisine;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class CuisinesController : ApiController
    {
        public CuisinesController(ISender sender) : base(sender)
        {
        }

        [HttpPost]
        public async Task<IActionResult> AddCuisine([FromBody] AddCuisineRequest addCuisineRequest, CancellationToken cancellationToken)
        {
            var command = new AddCuisineCommand(addCuisineRequest.Name);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCuisine(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteCuisineCommand(id);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }
    }
}