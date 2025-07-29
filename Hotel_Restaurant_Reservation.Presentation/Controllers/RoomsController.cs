using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.UpdateRoom;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AssignRoomTypeToRoom;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.RemoveRoomTypeFromRoom;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetAllRooms;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetRoomById;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetAmenitiesByRoomId;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class RoomsController : ApiController
    {
        public RoomsController(ISender sender) : base(sender)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms(CancellationToken cancellationToken)
        {
            var query = new GetAllRoomsQuery();
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetRoomById))]
        public async Task<IActionResult> GetRoomById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetRoomByIdQuery(id);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
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

        [HttpDelete("{roomId:guid}/remove-room-type")]
        public async Task<IActionResult> RemoveRoomTypeFromRoom(Guid roomId, CancellationToken cancellationToken)
        {
            var command = new RemoveRoomTypeFromRoomCommand(roomId);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }

        [HttpGet("{roomId:guid}/amenities")]
        public async Task<IActionResult> GetAmenitiesByRoomId(Guid roomId, CancellationToken cancellationToken)
        {
            var query = new GetAmenitiesByRoomIdQuery(roomId);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}