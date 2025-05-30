using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CuisineDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.CurrencyTypeDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.DishDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.FeatureDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.MealTypeDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.RestaurantDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.TagDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.WorkTimeDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries;
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
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveDishesFromRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveFeaturesFromRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveMealTypesFromRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveTagsFromRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveWorkTimesFromRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetAllRestaurants;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantById;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
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
        GetAllRestaurantsQuery query = new GetAllRestaurantsQuery(tagId, featureId, cuisineId, dishId, mealTypeId,
            countryId, cityId, localLocationId, minPrice, maxPrice, minStarRating, maxStarRating);

        IEnumerable<Restaurant>? restaurants = await Sender.Send(query, cancellationToken);

        IEnumerable<RestaurantResponse> restaurantResponses = new List<RestaurantResponse>();

        if (restaurants != null)
        {
            restaurantResponses = mapper.Map<IEnumerable<RestaurantResponse>>(restaurants);
        }

        return Ok(restaurantResponses);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ActionName("GetRestaurantById")]
    public async Task<IActionResult> GetRestaurantById(Guid id, CancellationToken cancellationToken)
    {
        GetRestaurantByIdQuery query = new GetRestaurantByIdQuery(id);

        Restaurant? restaurant = await Sender.Send(query, cancellationToken);

        if(restaurant is null) 
            return NotFound();

        var restaurantResponse = mapper.Map<RestaurantResponse>(restaurant);

        return Ok(restaurantResponse);
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
    public async Task<IActionResult> DeleteRestaurant(Guid id,  CancellationToken cancellationToken)
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
        var newRestaurant = mapper.Map<Restaurant>(restaurantUpdateRequest);

        var command = new UpdateRestaurantCommand(id, newRestaurant);

        var updatedRestaurant = await Sender.Send(command, cancellationToken);

        if (updatedRestaurant == null)
            return NotFound();

        var updatedRestaurantResponse = mapper.Map<RestaurantResponse>(updatedRestaurant);

        return Ok(updatedRestaurantResponse);
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

        if(result.IsFailure)
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
        [FromBody] RemoveCurrencyTypesFromRestaurant removeCuisineFromRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new RemoveCurrencyTypesFromResaturantCommand(restaurantId, removeCuisineFromRestaurantRequest.Ids);

        var currencyTypes = await Sender.Send(command, cancellationToken);

        var currencyTypeResponses = mapper.Map<IEnumerable<CurrencyTypeResponse>>(currencyTypes);

        return Ok(currencyTypeResponses);
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
        var command = new RemoveDishesFromRestaurantCommand(restaurantId, removeDishesWithPricesFormRestaurantRequest.Ids);

        var dishesWithPrices = await Sender.Send(command, cancellationToken);

        var dishesWithPriceResponses = new List<DishWithPriceResponse>();

        foreach (var (dish, price) in dishesWithPrices)
        {
            //dishesWithPriceResponses.Add(new DishWithPriceResponse()
            //{
            //    Id = dish.Id,
            //    Price = price,
            //    Name = dish.Name,
            //});
        }

        return Ok(dishesWithPriceResponses);
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
        var command = new RemoveFeaturesFromRestaurantCommand(restaurantId, removeFeaturesFromRestaurantRequest.Ids);

        var features = await Sender.Send(command, cancellationToken);

        var featureResponses = mapper.Map<IEnumerable<FeatureResponse>>(features);

        return Ok(featureResponses);
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
        var command = new RemoveMealTypesFromRestaurantCommand(restaurantId, removeMealTypesFromRestaurantRequest.Ids);

        var mealTypes = await Sender.Send(command, cancellationToken);

        var mealTypeResponses = mapper.Map<IEnumerable<MealTypeResponse>>(mealTypes);

        return Ok(mealTypeResponses);
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
        var command = new RemoveTagsFromRestaurantCommand(restaurantId, removeTagsFromRestaurantRequest.Ids);

        var tags = await Sender.Send(command, cancellationToken);

        var tagsResponses = mapper.Map<IEnumerable<TagResponse>>(tags);

        return Ok(tagsResponses);
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
        var command = new RemoveWorkTimesFromRestaurantCommand(restaurantId, removeworkTimeFromRestaurantRequest.Ids);

        var workTimes = await Sender.Send(command, cancellationToken);

        var workTimeResponses = mapper.Map<IEnumerable<WorkTimeResponse>>(workTimes);

        return Ok(workTimeResponses);
    }
}
