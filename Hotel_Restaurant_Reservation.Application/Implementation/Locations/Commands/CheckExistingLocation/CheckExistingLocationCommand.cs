using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.CheckExistingLocation;

public class CheckExistingLocationCommand : ICommand<Result<Guid>>
{
    public CheckExistingLocationCommand(CheckExistingLocationRequest checkExistingLocationRequest)
    {
        CheckExistingLocationRequest = checkExistingLocationRequest;
    }

    public CheckExistingLocationRequest CheckExistingLocationRequest { get; }
}
