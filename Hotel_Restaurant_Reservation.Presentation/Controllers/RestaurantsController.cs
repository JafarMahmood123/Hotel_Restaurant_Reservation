using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.MealTypeDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.Restaurant;
using Hotel_Restaurant_Reservation.Application.DTOs.RestaurantDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.AddCity;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.AddCountry;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.AddLocalLocation;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.AddLocationCommand;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetAllRestaurants;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantById;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class RestaurantsController : ApiController
{
    private readonly IMapper _mapper;

    public RestaurantsController(ISender sender, IMapper mapper) : base(sender)
    {
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllRestaurants(CancellationToken cancellationToken)
    {
        GetAllRestaurantsQuery query = new GetAllRestaurantsQuery();

        IEnumerable<Restaurant>? restaurants = await Sender.Send(query, cancellationToken);

        IEnumerable<RestaurantResponse> restaurantResponses = new List<RestaurantResponse>();

        if (restaurants != null)
        {
            restaurantResponses = _mapper.Map<IEnumerable<RestaurantResponse>>(restaurants);
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

        var restaurantResponse = _mapper.Map<RestaurantResponse>(restaurant);

        return Ok(restaurantResponse);
    }

    [HttpPost]
    public async Task<IActionResult> AddRestaurant(RestaurantAddRequest restaurantAddRequest, CancellationToken cancellationToken)
    {
        Country country = _mapper.Map<Country>(restaurantAddRequest.CountryRequest);

        City city = _mapper.Map<City>(restaurantAddRequest.CityRequest);

        LocalLocation localLocation= _mapper.Map<LocalLocation>(restaurantAddRequest.LocalLocationRequest);

        Restaurant restaurant = _mapper.Map<Restaurant>(restaurantAddRequest);

        Location location = new Location();

        var countryCommand = new AddCountryCommand(country);
        country = await Sender.Send(countryCommand, cancellationToken);

        var cityCommand = new AddCityCommand(city);
        city = await Sender.Send(cityCommand, cancellationToken);

        var localLocationCommand = new AddLocalLocationCommand(localLocation);
        localLocation = await Sender.Send(localLocationCommand, cancellationToken);

        var locationCommand = new AddLocationCommand(country, city, localLocation);
        location = await Sender.Send(locationCommand, cancellationToken);

        var restaurantCommand = new AddRestaurantCommand(restaurant, location);
        restaurant = await Sender.Send(restaurantCommand, cancellationToken);

        return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.Id }, restaurant);
    }
}
