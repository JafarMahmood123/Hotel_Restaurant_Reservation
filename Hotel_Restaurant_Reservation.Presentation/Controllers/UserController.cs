using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadUserImages;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetUserImagesByUserId;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.ChangePassword;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.DeleteCustomer;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.LogIn;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.SignUp;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.UpdateCustomer;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Queries.GetAllUsers;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Queries.GetUserById;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class UserController : ApiController
{
    public UserController(ISender sender) : base(sender)
    {
    }

    [Authorize(Roles = "Admin, Customer")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllUser(CancellationToken cancellationToken)
    {
        var query = new GetAllUsersQuery();
        var result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
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

    [Authorize(Roles = "Admin, Customer")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand(id, request);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin, Customer")]
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest, CancellationToken cancellationToken)
    {
        var command = new ChangePasswordCommand(changePasswordRequest);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }

    [Authorize(Roles = "Admin, Customer")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(id);
        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return NoContent();
    }

    [Authorize(Roles = "Customer")]
    [HttpPost("{userId:guid}/images")]
    public async Task<IActionResult> UploadUserImages(Guid userId, [FromForm] List<UploadImageRequest> imageFiles, CancellationToken cancellationToken)
    {
        if (imageFiles == null || imageFiles.Count == 0)
        {
            return BadRequest("No files were uploaded.");
        }

        var command = new UploadUserImagesCommand
        {
            UserId = userId,
            ImageFiles = imageFiles
        };

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("{userId:guid}/images")]
    public async Task<IActionResult> GetUserImages(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetUserImagesByUserIdQuery(userId);
        var result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }
}