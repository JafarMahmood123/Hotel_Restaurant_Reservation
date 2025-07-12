using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.UpdateRoom;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AssignRoomTypeToRoom;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class RoomsController : ApiController
    {
        public RoomsController(ISender sender) : base(sender)
        {
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] UpdateRoomRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateRoomCommand(id, request);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost("{roomId:guid}/assign-room-type/{roomTypeId:guid}")]
        public async Task<IActionResult> AssignRoomTypeToRoom(Guid roomId, Guid roomTypeId, CancellationToken cancellationToken)
        {
            var command = new AssignRoomTypeToRoomCommand(roomId, roomTypeId);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }
    }
}