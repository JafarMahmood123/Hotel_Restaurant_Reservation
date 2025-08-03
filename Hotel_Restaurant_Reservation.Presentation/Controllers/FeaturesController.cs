using Hotel_Restaurant_Reservation.Application.Implementation.Features.Commands.AddFeature;
using Hotel_Restaurant_Reservation.Application.Implementation.Features.Queries.GetAllFeatures;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class FeaturesController : ApiController
{
    public FeaturesController(ISender sender) : base(sender)
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

    [HttpGet]
    public async Task<IActionResult> GetAllFeatures(CancellationToken cancellationToken)
    {
        var query = new GetAllFeaturesQuery();

        var result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
