using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.RestaurantBookingDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.AddRestaurantBooking;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
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
            return BadRequest();

        var restaurantBooking = mapper.Map<RestaurantBooking>(addRestaurantBookingRequest);

        var restaurantBookingddCommand = new AddRestaurantBookingCommand(restaurantBooking);

        restaurantBooking = await Sender.Send(restaurantBookingddCommand, cancellationToken);

        if(restaurantBookingddCommand is not null)
        {
            var restaurantBookingResponse = mapper.Map<RestaurantBookingResponse>(restaurantBooking);

            return Ok(restaurantBookingResponse);
        }


        //Need something to tell that thi table is resrved at this time
        return BadRequest();
    }
}
