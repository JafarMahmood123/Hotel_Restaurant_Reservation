using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.ChangePassword;
using Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.LogIn;
using Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.SignUp;
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

        var command = new LogInCommand(logInRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest, CancellationToken cancellationToken)
    {
        var command = new SignUpCommand(signUpRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest, CancellationToken cancellationToken)
    {
        var command = new ChangePasswordCommand(changePasswordRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }
}