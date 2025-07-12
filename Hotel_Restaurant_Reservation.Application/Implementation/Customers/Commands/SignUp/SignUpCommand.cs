using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Customers.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.SignUp;

public class SignUpCommand : ICommand<Result<CustomerResponse>>
{
    public SignUpCommand(SignUpRequest signUpRequest) 
    {
        SignUpRequest = signUpRequest;
    }

    public SignUpRequest SignUpRequest { get; }
}
