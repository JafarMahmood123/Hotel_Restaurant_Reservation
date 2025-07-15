using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Commands;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Commands.AddMealType;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries.GetAllMealTypes;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries.GetAllMealTypesByRestaurantId;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class MealTypeController : ApiController
    {
        public MealTypeController(ISender sender) : base(sender)
        {
        }

        [HttpPost]
        public async Task<IActionResult> AddMealType([FromBody] AddMealTypeRequest addMealTypeRequest, CancellationToken cancellationToken)
        {
            var command = new AddMealTypeCommand(addMealTypeRequest);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);


            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMealTypes(CancellationToken cancellationToken)
        {
            var query = new GetAllMealTypesQuery();
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("restaurant/{restaurantId:guid}")]
        public async Task<IActionResult> GetAllMealTypesByRestaurantId(Guid restaurantId, CancellationToken cancellationToken)
        {
            var query = new GetAllMealTypesByRestaurantIdQuery(restaurantId);
            var result = await Sender.Send(query, cancellationToken);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
