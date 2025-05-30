using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.RestaurantBookingDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Commands.AddBookingDishes;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.AddRestaurantBooking;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries.GetRestaurantBookingsByCustomerId;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class RestaurantBookingController : ApiController
{
    private readonly IMapper _mapper;

    public RestaurantBookingController(ISender sender, IMapper mapper) : base(sender)
    {
        this._mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddRestaurantBooking(AddRestaurantBookingRequest addRestaurantBookingRequest
        , CancellationToken cancellationToken)
    {
        if (!addRestaurantBookingRequest.AddBookingDishRequest.Any())
            return BadRequest("You have to add some dishes.");

        var restaurantBooking = _mapper.Map<RestaurantBooking>(addRestaurantBookingRequest);

        var restaurantBookingddCommand = new AddRestaurantBookingCommand(restaurantBooking);

        restaurantBooking = await Sender.Send(restaurantBookingddCommand, cancellationToken);

        if (restaurantBooking is null)
            return BadRequest("This table is reserved before.");

        var restaurantBookingResponse = _mapper.Map<RestaurantBookingResponse>(restaurantBooking);

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

        var restaurantBookingResponses = _mapper.Map<IEnumerable<RestaurantBookingResponse>>(restaurantBookings);


        return Ok(restaurantBookingResponses);
    }

    [HttpPost]
    [Route("{bookingId:guid}")]
    public async Task<IActionResult> AddDishesToBooking(Guid bookingId, AddBookingDishesRequest addBookingDishesRequest,
        CancellationToken cancellationToken)
    {
        var command = new AddBookingDishesCommand(bookingId, addBookingDishesRequest);

        var bookingResponeses = await Sender.Send(command, cancellationToken);

        return Ok(bookingResponeses);
    }
}
