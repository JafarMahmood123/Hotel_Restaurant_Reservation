﻿using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CuisineDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.FeatureDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRestaurantImage;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetRestaurantImagesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCuisinesToRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCurrencyTypesToRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddDishesToRestaurant;
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
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetAllRestaurants;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetFeaturesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantById;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantCuisinesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantDishesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetWorkTimesByRestaurantId;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries.GetTagsByRestaurantId;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
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

    [HttpPost]
    public async Task<IActionResult> AddRestaurant([FromBody] AddRestaurantRequest restaurantAddRequest, CancellationToken cancellationToken)
    {

        var command = new AddRestaurantCommand(restaurantAddRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetRestaurantById), new { id = result.Value.Id }, result.Value);
    }

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

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] Guid id, [FromBody] RestaurantUpdateRequest restaurantUpdateRequest,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRestaurantCommand(id, restaurantUpdateRequest);
        var result = await Sender.Send(command);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    [Route("{restaurantId:guid}/cuisines")]
    public async Task<IActionResult> AddCuisineToRestaurant([FromRoute] Guid restaurantId,
        [FromBody] AddCuisineToRestaurantRequest addCuisineToRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new AddCuisinesToRestaurantCommand(restaurantId, addCuisineToRestaurantRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete]
    [Route("{restaurantId:guid}/cuisines")]
    public async Task<IActionResult> RemoveCuisinesFromRestaurant([FromRoute] Guid restaurantId,
        [FromBody] RemoveCuisineFromRestaurantRequest removeCuisineFromRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new RemoveCuisinesFromRestaurantCommand(restaurantId, removeCuisineFromRestaurantRequest);

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

    [HttpPost]
    [Route("{restaurantId:guid}/dishes")]
    public async Task<IActionResult> AddDishesWithPricesToRestaurant([FromRoute] Guid restaurantId,
        [FromBody] AddDishesWithPricesToRestaurantRequest addDishesWithPricesToRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new AddDishesToRestaurantCommand(restaurantId, addDishesWithPricesToRestaurantRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete]
    [Route("{restaurantId:guid}/dishes")]
    public async Task<IActionResult> RemoveDishesWithPricesFromRestaurant([FromRoute] Guid restaurantId,
        [FromBody] RemoveDishesFromRestaurantRequest removeDishesWithPricesFormRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new RemoveDishesFromRestaurantCommand(restaurantId, removeDishesWithPricesFormRestaurantRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    [Route("{restaurantId:guid}/features")]
    public async Task<IActionResult> AddFeaturesToRestaurant([FromRoute] Guid restaurantId,
        [FromBody] AddFeaturesToRestaurantRequest addFeaturesToRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new AddFeaturesToRestaurantCommand(restaurantId, addFeaturesToRestaurantRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }


    [HttpDelete]
    [Route("{restaurantId:guid}/features")]
    public async Task<IActionResult> RemoveFeaturesFromRestaurant([FromRoute] Guid restaurantId,
        [FromBody] RemoveFeaturesFromRestaurantRequest removeFeaturesFromRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new RemoveFeaturesFromRestaurantCommand(restaurantId, removeFeaturesFromRestaurantRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{restaurantId}/features")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFeaturesByRestaurantId(Guid restaurantId, CancellationToken cancellationToken)
    {
        var query = new GetFeaturesByRestaurantIdQuery(restaurantId);
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result.Value);
    }


    [HttpPost]
    [Route("{restaurantId:guid}/mealTypes")]
    public async Task<IActionResult> AddMealTypesToRestaurant([FromRoute] Guid restaurantId,
        [FromBody] AddMealTypesToRestaurantRequest addMealTypesToRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new AddMealTypesToRestaurantCommand(restaurantId, addMealTypesToRestaurantRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete]
    [Route("{restaurantId:guid}/mealTypes")]
    public async Task<IActionResult> RemoveMealTypesFromRestaurant([FromRoute] Guid restaurantId,
        [FromBody] RemoveMealTypesFromRestaurantRequest removeMealTypesFromRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new RemoveMealTypesFromRestaurantCommand(restaurantId, removeMealTypesFromRestaurantRequest);
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

    [HttpPost]
    [Route("{restaurantId:guid}/tags")]
    public async Task<IActionResult> AddTagsToRestaurant([FromRoute] Guid restaurantId,
        [FromBody] AddTagsToRestaurantRequest addTagsToRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new AddTagsToRestaurantCommand(restaurantId, addTagsToRestaurantRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete]
    [Route("{restaurantId:guid}/tags")]
    public async Task<IActionResult> RemoveTagsFromRestaurant([FromRoute] Guid restaurantId,
        [FromBody] RemoveTagsFromRestaurantRequest removeTagsFromRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new RemoveTagsFromRestaurantCommand(restaurantId, removeTagsFromRestaurantRequest);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
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

    [HttpPost]
    [Route("{restaurantId:guid}/workTimes")]
    public async Task<IActionResult> AddWorkTimeToRestaurant([FromRoute] Guid restaurantId,
        [FromBody] AddWorkTimesToRestaurantRequest addworkTimeToRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new AddWorkTimesToRestaurantCommand(restaurantId, addworkTimeToRestaurantRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete]
    [Route("{restaurantId:guid}/workTimes")]
    public async Task<IActionResult> RemoveWorkTimeTFromRestaurant([FromRoute] Guid restaurantId,
        [FromBody] RemoveWorkTimesFromRestaurantRequest removeworkTimeFromRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new RemoveWorkTimesFromRestaurantCommand(restaurantId, removeworkTimeFromRestaurantRequest);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
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

    [HttpPost("{restaurantId:guid}/images")]
    public async Task<IActionResult> UploadRestaurantImages(Guid restaurantId, [FromForm] List<UploadImageRequest> imageFiles, CancellationToken cancellationToken)
    {
        if (imageFiles == null || imageFiles.Count == 0)
        {
            return BadRequest("No files were uploaded.");
        }

        var command = new UploadRestaurantImagesCommand
        {
            RestaurantId = restaurantId,
            ImageFiles = imageFiles
        };

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
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
}
