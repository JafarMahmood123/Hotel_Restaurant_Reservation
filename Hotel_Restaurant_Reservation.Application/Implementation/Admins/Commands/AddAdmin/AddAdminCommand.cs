using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Admins.Commands.AddAdmin;

public class AddAdminCommand : ICommand<Result>
{
    public AddAdminCommand(AddAdminRequest addAdminRequest)
    {
        AddAdminRequest = addAdminRequest;
    }

    public AddAdminRequest AddAdminRequest { get; }
}
