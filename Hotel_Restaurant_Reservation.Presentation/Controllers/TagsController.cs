using Hotel_Restaurant_Reservation.Application.DTOs.TagDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Commands.AddTag;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

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
}
