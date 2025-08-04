using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries.GetCurrencyTypesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.RemoveRestaurantImage;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRestaurantDishImage;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRestaurantImage;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetRestaurantDishImage;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetRestaurantImagesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCuisinesToRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCurrencyTypesToRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddDishesToRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddDishToRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddFeaturesToRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddMealTypesToRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddTagsToRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddWorkTimesToRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.DeleteRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCuisinesFromRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCurrencyTypesFromResaturant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCurrencyTypesFromRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveDishesFromRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveFeaturesFromRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveMealTypesFromRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveTagsFromRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveWorkTimesFromRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurantDish;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetAllRestaurants;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetFeaturesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantById;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantCuisinesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantDishesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetWorkTimesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries.GetTagsByRestaurantId;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using Hotel_Restaurant_Reservation.Presentation.ApiModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class RestaurantsController : ApiController
{
    private readonly IMapper mapper;

    public RestaurantsController(ISender sender, IMapper mapper) : base(sender)
    {
        this.mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllRestaurants(Guid? tagId, Guid? featureId, Guid? cuisineId, Guid? countryId,
        Guid? cityId, Guid? localLocationId, Guid? dishId, Guid? mealTypeId, CancellationToken cancellationToken,
        double? minPrice = 0, double? maxPrice = double.MaxValue, double? minStarRating = 0, double? maxStarRating = 5)
    {
        var query = new GetAllRestaurantsQuery(
        tagId, featureId, cuisineId, dishId, mealTypeId,
        countryId, cityId, localLocationId,
        minPrice, maxPrice, minStarRating, maxStarRating);

        var result = await Sender.Send(query);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ActionName("GetRestaurantById")]
    public async Task<IActionResult> GetRestaurantById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetRestaurantByIdQuery(id);

        var result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddRestaurant([FromBody] AddRestaurantRequest restaurantAddRequest, CancellationToken cancellationToken)
    {

        var command = new AddRestaurantCommand(restaurantAddRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetRestaurantById), new { id = result.Value.Id }, result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteRestaurant(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteRestaurantCommand(id);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] Guid id, [FromBody] UpdateRestaurantRequest restaurantUpdateRequest,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRestaurantCommand(id, restaurantUpdateRequest);
        var result = await Sender.Send(command);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("{restaurantId:guid}/cuisines/{cuisineId:guid}")]
    public async Task<IActionResult> AddCuisineToRestaurant([FromRoute] Guid restaurantId, [FromRoute] Guid cuisineId,
        CancellationToken cancellationToken)
    {
        var command = new AddCuisinesToRestaurantCommand(restaurantId, cuisineId);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{restaurantId:guid}/cuisines/{cuisineId:guid}")]
    public async Task<IActionResult> RemoveCuisinesFromRestaurant([FromRoute] Guid restaurantId, [FromRoute] Guid cuisineId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveCuisinesFromRestaurantCommand(restaurantId, cuisineId);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    [Route("{restaurantId:guid}/cuisines")]
    public async Task<IActionResult> GetRestaurantCuisinesByRestaurantId([FromRoute] Guid restaurantId, CancellationToken cancellationToken)
    {
        var query = new GetRestaurantCuisinesByRestaurantIdQuery(restaurantId);

        var result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("{restaurantId:guid}/currencyTypes")]
    public async Task<IActionResult> AddCurrencyTypesToRestaurant([FromRoute] Guid restaurantId,
        [FromBody] AddCurrencyTypeToRestaurantRequest addCurrencyTypeToRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new AddCurrencyTypesToRestaurantCommand(restaurantId, addCurrencyTypeToRestaurantRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{restaurantId:guid}/currencyTypes")]
    public async Task<IActionResult> RemoveCurrencyTypesFromRestaurant([FromRoute] Guid restaurantId,
        [FromBody] RemoveCurrencyTypesFromRestaurantRequest removeCuisineFromRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new RemoveCurrencyTypesFromRestaurantCommand(restaurantId, removeCuisineFromRestaurantRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    [Route("{restaurantId:guid}/currencyTypes")]
    public async Task<IActionResult> GetCurrencyTypesByRestaurantId([FromRoute] Guid restaurantId, CancellationToken cancellationToken)
    {
        var query = new GetCurrencyTypesByRestaurantIdQuery(restaurantId);

        var result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("{restaurantId:guid}/dishes")]
    public async Task<IActionResult> AddDishesWithPricesToRestaurant([FromRoute] Guid restaurantId,
        [FromBody] AddDishToRestaurantRequest addDishToRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new AddDishToRestaurantCommand(restaurantId, addDishToRestaurantRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{restaurantId:guid}/dishes/{dishId:guid}")]
    public async Task<IActionResult> RemoveDishFromRestaurant([FromRoute] Guid restaurantId, [FromRoute] Guid dishId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveDishFromRestaurantCommand(restaurantId, dishId);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("{restaurantId:guid}/dishes/{dishId:guid}")]
    public async Task<IActionResult> UpdateRestaurantDish([FromRoute] Guid restaurantId, [FromRoute] Guid dishId,
        [FromBody] UpdateRestaurantDishRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateRestaurantDishCommand(restaurantId, dishId, request);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("{restaurantId:guid}/features/{featureId:guid}")]
    public async Task<IActionResult> AddFeaturesToRestaurant([FromRoute] Guid restaurantId, [FromRoute] Guid featureId, CancellationToken cancellationToken)
    {
        var command = new AddFeaturesToRestaurantCommand(restaurantId, featureId);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{restaurantId:guid}/features/{featureId:guid}")]
    public async Task<IActionResult> RemoveFeaturesFromRestaurant([FromRoute] Guid restaurantId, [FromRoute] Guid featureId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveFeaturesFromRestaurantCommand(restaurantId, featureId);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{restaurantId}/features")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFeatureByRestaurantId([FromRoute] Guid restaurantId, CancellationToken cancellationToken)
    {
        var query = new GetFeaturesByRestaurantIdQuery(restaurantId);
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("{restaurantId:guid}/mealTypes/{mealTypeId:guid}")]
    public async Task<IActionResult> AddMealTypesToRestaurant([FromRoute] Guid restaurantId, [FromRoute] Guid mealTypeId, CancellationToken cancellationToken)
    {
        var command = new AddMealTypesToRestaurantCommand(restaurantId, mealTypeId);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{restaurantId:guid}/mealTypes/{mealTypeId:guid}")]
    public async Task<IActionResult> RemoveMealTypesFromRestaurant([FromRoute] Guid restaurantId, [FromRoute] Guid mealTypeId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveMealTypesFromRestaurantCommand(restaurantId, mealTypeId);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    [Route("{restaurantId:guid}/mealTypes")]
    public async Task<IActionResult> GetMealTypesByRestaurantId(Guid restaurantId, CancellationToken cancellationToken)
    {
        var query = new GetMealTypesByRestaurantIdQuery(restaurantId);
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("{restaurantId:guid}/tags/{tagId:guid}")]
    public async Task<IActionResult> AddTagsToRestaurant([FromRoute] Guid restaurantId, [FromRoute] Guid tagId, CancellationToken cancellationToken)
    {
        var command = new AddTagsToRestaurantCommand(restaurantId, tagId);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{restaurantId:guid}/tags/{tagId:guid}")]
    public async Task<IActionResult> RemoveTagFromRestaurant([FromRoute] Guid restaurantId, [FromRoute] Guid tagId
        , CancellationToken cancellationToken)
    {
        var command = new RemoveTagsFromRestaurantCommand(restaurantId, tagId);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return NoContent();
    }

    [HttpGet]
    [Route("{restaurantId:guid}/tags")]
    public async Task<IActionResult> GetTagsByRestaurantId(Guid restaurantId, CancellationToken cancellationToken)
    {
        var query = new GetTagsByRestaurantIdQuery(restaurantId);
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("{restaurantId:guid}/workTimes")]
    public async Task<IActionResult> AddWorkTimeToRestaurant([FromRoute] Guid restaurantId, [FromBody] AddWorkTimesToRestaurantRequest request
        , CancellationToken cancellationToken)
    {
        var command = new AddWorkTimesToRestaurantCommand(restaurantId, request);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("workTimes/{workTimeId:guid}")]
    public async Task<IActionResult> RemoveWorkTimeTFromRestaurant([FromRoute] Guid workTimeId, CancellationToken cancellationToken)
    {
        var command = new RemoveWorkTimesFromRestaurantCommand(workTimeId);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpGet("{restaurantId:guid}/workTimes")]
    public async Task<IActionResult> GetWorkTimesByRestaurantId(Guid restaurantId, CancellationToken cancellationToken)
    {
        var query = new GetWorkTimesByRestaurantIdQuery(restaurantId);
        var result = await Sender.Send(query, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{restaurantId:guid}/images")]
    public async Task<IActionResult> UploadRestaurantImage(Guid restaurantId, [FromForm] UploadImageApiRequest request, CancellationToken cancellationToken)
    {
        // The command now gets the file from the request model, which solves the Swagger UI issue.
        var command = new UploadRestaurantImageCommand
        {
            RestaurantId = restaurantId,
            ImageFile = request.ImageFile
        };

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        // On success, return a JSON object with the image URL.
        return Ok(new { imageUrl = result.Value });
    }

    [HttpGet("{restaurantId:guid}/images")]
    public async Task<IActionResult> GetRestaurantImages(Guid restaurantId, CancellationToken cancellationToken)
    {
        var query = new GetRestaurantImagesByRestaurantIdQuery(restaurantId);
        var result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("images")] // The route no longer takes an ID in the path
    public async Task<IActionResult> RemoveRestaurantImage([FromBody] RemoveImageApiRequest request, CancellationToken cancellationToken)
    {
        // The command is created from the request body's ImageUrl property
        var command = new RemoveRestaurantImageCommand(request.ImageUrl);
        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            // Handle NotFound or other errors
            return BadRequest(result.Error);
        }

        return NoContent(); // 204 No Content is a standard response for a successful deletion.
    }

    [HttpGet("{restaurantId:guid}/dishes")]
    public async Task<IActionResult> GetRestaurantDishesByRestaurnatId(Guid restaurantId, CancellationToken cancellationToken)
    {
        var query = new GetRestaurantDishesByRestaurantIdQuery(restaurantId);
        var result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{restaurantId:guid}/dishes/{dishId:guid}/image")]
    public async Task<IActionResult> UploadDishImage(Guid restaurantId, Guid dishId, [FromForm] UploadImageApiRequest removeImageApiRequest, CancellationToken cancellationToken)
    {
        var command = new UploadRestaurantDishImageCommand
        {
            RestaurantId = restaurantId,
            DishId = dishId,
            ImageFile = removeImageApiRequest.ImageFile
        };

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(new { imageUrl = result.Value });
    }

    [HttpGet("{restaurantId:guid}/dishes/{dishId:guid}/image")]
    public async Task<IActionResult> GetDishImage(Guid restaurantId, Guid dishId, CancellationToken cancellationToken)
    {
        var query = new GetRestaurantDishImageQuery(restaurantId, dishId);

        var result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}
