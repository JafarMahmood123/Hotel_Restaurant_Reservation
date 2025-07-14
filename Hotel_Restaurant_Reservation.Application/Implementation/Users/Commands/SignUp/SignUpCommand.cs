using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.SignUp;

public class SignUpCommand : ICommand<Result<UserResponse>>
{
    public SignUpCommand(SignUpRequest signUpRequest)
    {
        SignUpRequest = signUpRequest;
    }

    public SignUpRequest SignUpRequest { get; }
}