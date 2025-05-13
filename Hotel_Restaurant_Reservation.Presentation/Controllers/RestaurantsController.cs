using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CuisineDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.Restaurant;
using Hotel_Restaurant_Reservation.Application.DTOs.RestaurantDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCuisinesToRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.DeleteRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCuisineFromRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetAllRestaurants;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantById;
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
    public async Task<IActionResult> GetAllRestaurants(Guid? tagId, Guid? featureId, Guid? cuisineId, Guid? countryId, Guid? cityLocalLocationId,
        Guid? dishId, Guid? mealTypeId, CancellationToken cancellationToken, double? minPrice = 0,
        double? maxPrice = double.MaxValue, double? minStarRating = 0, double? maxStarRating = 5)
    {
        GetAllRestaurantsQuery query = new GetAllRestaurantsQuery(tagId, featureId, cuisineId, dishId, mealTypeId,
            countryId, cityLocalLocationId, minPrice, maxPrice, minStarRating, maxStarRating);

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
    public async Task<IActionResult> AddRestaurant([FromBody] RestaurantAddRequest restaurantAddRequest, CancellationToken cancellationToken)
    {
        Restaurant restaurant = mapper.Map<Restaurant>(restaurantAddRequest);

        Location location = mapper.Map<Location>(restaurantAddRequest.addLocationRequest);

        var command = new AddRestaurantCommand(restaurant, location);

        restaurant = await Sender.Send(command, cancellationToken);

        var restaurantResponse = mapper.Map<RestaurantResponse>(restaurant);

        return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.Id }, restaurantResponse);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteRestaurant(Guid id,  CancellationToken cancellationToken)
    {
        var command = new DeleteRestaurantCommand(id);

        var deletedRestaurant = await Sender.Send(command, cancellationToken);

        if(deletedRestaurant == null)
            return NotFound();

        var deletedRestaurantResponse = mapper.Map<RestaurantResponse>(deletedRestaurant);

        return Ok(deletedRestaurantResponse);
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
        var command = new AddCuisineToRestaurantCommand(restaurantId, addCuisineToRestaurantRequest.Ids);

        var cuisine = await Sender.Send(command, cancellationToken);

        var cuisineResponse = mapper.Map<IEnumerable<CuisineResponse>>(cuisine);

        return Ok(cuisineResponse);
    }

    [HttpDelete]
    [Route("{restaurantId:guid}/cuisines")]
    public async Task<IActionResult> RemoveCuisineFromRestaurant([FromRoute] Guid restaurantId, 
        [FromBody] RemoveCuisineFromRestaurantRequest removeCuisineFromRestaurantRequest, CancellationToken cancellationToken)
    {
        var command = new RemoveCuisineFromRestaurantCommand(restaurantId, removeCuisineFromRestaurantRequest.Id);

        var cuisine = await Sender.Send(command, cancellationToken);

        var cuisineResponse = mapper.Map<CuisineResponse>(cuisine);

        return Ok(cuisineResponse);
    }
}
