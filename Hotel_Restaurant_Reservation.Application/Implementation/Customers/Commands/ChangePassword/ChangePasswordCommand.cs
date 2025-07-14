using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.ChangePassword;

public class ChangePasswordCommand : ICommand<Result>
{
    public ChangePasswordCommand(ChangePasswordRequest changePasswordRequest)
    {
        ChangePasswordRequest = changePasswordRequest;
    }

    public ChangePasswordRequest ChangePasswordRequest { get; }
}