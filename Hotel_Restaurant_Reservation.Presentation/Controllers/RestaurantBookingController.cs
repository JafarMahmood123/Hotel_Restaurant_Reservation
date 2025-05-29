using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.RestaurantBookingDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.AddRestaurantBooking;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries.GetRestaurantBookingsByCustomerId;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantById;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class RestaurantBookingController : ApiController
{
    private readonly IMapper mapper;

    public RestaurantBookingController(ISender sender, IMapper mapper) : base(sender)
    {
        this.mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddRestaurantBooking(AddRestaurantBookingRequest addRestaurantBookingRequest
        , CancellationToken cancellationToken)
    {
        if (!addRestaurantBookingRequest.AddBookingDishRequest.Any())
            return BadRequest("You have to add some dishes.");

        var restaurantBooking = mapper.Map<RestaurantBooking>(addRestaurantBookingRequest);

        var restaurantBookingddCommand = new AddRestaurantBookingCommand(restaurantBooking);

        restaurantBooking = await Sender.Send(restaurantBookingddCommand, cancellationToken);

        if (restaurantBooking is null)
            return BadRequest("This table is reserved before.");

        var restaurantBookingResponse = mapper.Map<RestaurantBookingResponse>(restaurantBooking);

        return Ok(restaurantBookingResponse);
        //Need something to tell that thi table is resrved at this time

    }

    [HttpGet]
    [Route("{customerId:guid}")]
    public async Task<IActionResult> GetRestaurantBookingByCustomerId(Guid customerId, CancellationToken cancellationToken)
    {
        var query = new GetRestaurantBookingsByCustomerIdQuery(customerId);

        var restaurantBookings = await Sender.Send(query, cancellationToken);

        if (restaurantBookings is null)
            return NotFound();

        var restaurantBookingResponses = mapper.Map<IEnumerable<RestaurantBookingResponse>>(restaurantBookings);


        return Ok(restaurantBookingResponses);
    }
}
