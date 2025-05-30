using Hotel_Restaurant_Reservation.Application.Implementation.Features.Commands.AddFeature;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class FeatureController : ApiController
{
    public FeatureController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> AddFeature([FromBody] AddFeatureRequest addFeatureRequest, CancellationToken cancellationToken)
    {
        var command = new AddFeatureCommand(addFeatureRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
