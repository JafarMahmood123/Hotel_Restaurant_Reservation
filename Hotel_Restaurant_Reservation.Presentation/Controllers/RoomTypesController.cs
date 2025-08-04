using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries.GetRoomType;
using Hotel_Restaurant_Reservation.Application.Implementation.RoomTypes.Commands.AddRoomType;
using Hotel_Restaurant_Reservation.Application.Implementation.RoomTypes.Queries.GetAllRoomTypes;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class RoomTypesController : ApiController
{
    public RoomTypesController(ISender sender) : base(sender)
    {
    }

    [HttpGet("{roomTypeId:guid}")]
    public async Task<IActionResult> GetRoomType(Guid roomTypeId, CancellationToken cancellationToken)
    {
        var query = new GetRoomTypeQuery(roomTypeId);
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoomType(CancellationToken cancellationToken)
    {
        var query = new GetAllRoomTypesQuery();
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{description}")]
    public async Task<IActionResult> AddRoomType([FromRoute] string description, CancellationToken cancellationToken)
    {
        var command = new AddRoomTypeCommand(description);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }
}
