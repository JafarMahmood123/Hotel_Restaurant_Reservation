using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.RemoveRoomImage;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRoomImage;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetRoomImages;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AssignRoomTypeToRoom;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.RemoveRoomTypeFromRoom;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.UpdateRoom;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetAllRooms;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetAmenitiesByRoomId;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetRoomById;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using Hotel_Restaurant_Reservation.Presentation.ApiModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost("{roomId:guid}/images")]
        public async Task<IActionResult> UploadRoomImage(Guid roomId, [FromForm] UploadImageApiRequest request)
        {
            var command = new UploadRoomImageCommand(roomId, request);

            var result = await Sender.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("{roomId:guid}/images")]
        public async Task<IActionResult> GetRoomImages(Guid roomId)
        {
            var query = new GetRoomImagesQuery(roomId);
            var result = await Sender.Send(query);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("images")]
        public async Task<IActionResult> RemoveRoomImage([FromBody] RemoveImageApiRequest request)
        {
            var command = new RemoveRoomImageCommand(request.ImageUrl);
            var result = await Sender.Send(command);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }
}