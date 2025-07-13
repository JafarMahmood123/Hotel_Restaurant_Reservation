using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Commands.AddBookingDishes;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.AddRestaurantBooking;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.DeleteRestaurantBooking;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Queries.GetRestaurantBookingsByCustomerId;
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
    public async Task<IActionResult> AddRestaurantBooking([FromBody] AddRestaurantBookingRequest addRestaurantBookingRequest
        , CancellationToken cancellationToken)
    {
        var command = new AddRestaurantBookingCommand(addRestaurantBookingRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    [Route("{customerId:guid}")]
    public async Task<IActionResult> GetRestaurantBookingByCustomerId(Guid customerId, CancellationToken cancellationToken)
    {
        var query = new GetRestaurantBookingsByCustomerIdQuery(customerId);

        var result = await Sender.Send(query, cancellationToken);

        if (!result.Value.Any())
            return NoContent();

        return Ok(result.Value);
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRestaurantBooking(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteRestaurantBookingCommand(id);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return NoContent();
    }
}