using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.LogIn;

public class LogInCommand : ICommand<Result<string>>
{
    public LogInCommand(LogInRequest logInRequest)
    {
        LogInRequest = logInRequest;
    }

    public LogInRequest LogInRequest { get; }
}
