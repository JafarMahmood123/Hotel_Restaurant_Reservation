using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.Restaurant;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetAllRestaurants;
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

}
