using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CustomerDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.LogIn;
using Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.SignUp;
using Hotel_Restaurant_Reservation.Domain.Entities;
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
        if (!ModelState.IsValid)
            return BadRequest();

        var command = new LogInCommand(logInRequest.Email, logInRequest.Password);

        var token = await Sender.Send(command, cancellationToken);

        if (token == null)
            return BadRequest();

        return Ok(token);
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest, CancellationToken cancellationToken)
    {
        var customer = mapper.Map<Customer>(signUpRequest);

        var command = new SignUpCommand(customer);

        customer = await Sender.Send(command, cancellationToken);

        if (customer == null)
            return BadRequest("Your email is exist before.");

        var customerResponse = mapper.Map<CustomerResponse>(customer);
        return Ok(customerResponse);
    }
}
