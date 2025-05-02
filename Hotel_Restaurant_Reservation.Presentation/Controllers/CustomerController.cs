using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CustomerDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.LogIn;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class CustomerController : ApiController
{
    private readonly IMapper mapper;

    public CustomerController(ISender sender, IMapper mapper) : base(sender)
    {
        this.mapper = mapper;
    }

    [HttpPost("LogIn")]
    public async Task<IActionResult> LogIn([FromBody] LogInRequest logInRequest, CancellationToken cancellationToken)
    {
        var command = new LogInCommand(logInRequest.Email, logInRequest.Password);

        var token = await Sender.Send(command, cancellationToken);

        if (token == null)
            return BadRequest();

        return Ok(token);
    }
}
